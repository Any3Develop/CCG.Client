using System.Collections;
using CardGame.Attributes;
using TMPro;
using UnityEngine;

namespace CardGame.Layouts
{
    // 3rdParty , modified ArtemBrilev
    public class WarpTextExample : MonoBehaviour
    {

        private TMP_Text m_TextComponent;

        public AnimationCurve VertexCurve;
        public float CurveScale = 1.0f;

        [ExposeMethodInEditor]
        void Start()
        {
            m_TextComponent = gameObject.GetComponent<TMP_Text>();
            StartCoroutine(WarpText());
        }


        private AnimationCurve CopyAnimationCurve(AnimationCurve curve)
        {
            AnimationCurve newCurve = new AnimationCurve();

            newCurve.keys = curve.keys;

            return newCurve;
        }
        public Vector3 GetPointAtCurve(float time01)
        {
            time01 = Mathf.Clamp01(time01);
            var size = m_TextComponent.rectTransform.rect.width;
            var point = m_TextComponent.rectTransform.localPosition;
            point.x = Mathf.Lerp(-size/2f , size/2f, time01);
            point.y += VertexCurve.Evaluate(time01) * CurveScale;
            point.z = m_TextComponent.rectTransform.localPosition.z;
            return point;
        }
        
        public Vector2 GetNormalAtCurve(float time01)
        {
            time01 = Mathf.Clamp01(time01);
            var step = 1f / 25f;
            var timeNext = time01 + (time01 + step >= 1 ? 1 : step);
            var point1 = GetPointAtCurve(Mathf.Min(time01, timeNext));
            var point0 = GetPointAtCurve(Mathf.Max(time01, timeNext));
            var dir = point0 - point1;
            return Vector2.Perpendicular(dir.normalized);
        }
        
        public float GetCurveTime(float xPosition)
        {
            var size = m_TextComponent.rectTransform.rect.size;
            var center = m_TextComponent.rectTransform.rect.center;
            return Mathf.Clamp01(Mathf.InverseLerp(center.x - size.x/2f, center.x + size.x/2f, xPosition));
        }
        /// <summary>
        ///  Method to curve text along a Unity animation curve.
        /// </summary>
        /// <param name="textComponent"></param>
        /// <returns></returns>
        IEnumerator WarpText()
        {
            // VertexCurve.preWrapMode = WrapMode.Clamp;
            // VertexCurve.postWrapMode = WrapMode.Clamp;

            //Mesh mesh = m_TextComponent.textInfo.meshInfo[0].mesh;

            Vector3[] vertices;
            Matrix4x4 matrix;

            m_TextComponent.havePropertiesChanged = true; // Need to force the TextMeshPro Object to be updated.
            float old_CurveScale = CurveScale;
            AnimationCurve old_curve = CopyAnimationCurve(VertexCurve);

            while (true)
            {
                if (!m_TextComponent.havePropertiesChanged && old_CurveScale == CurveScale && old_curve.keys[1].value == VertexCurve.keys[1].value)
                {
                    yield return null;
                    continue;
                }

                old_CurveScale = CurveScale;
                old_curve = CopyAnimationCurve(VertexCurve);

                m_TextComponent.ForceMeshUpdate(); // Generate the mesh and populate the textInfo with data we can use and manipulate.

                TMP_TextInfo textInfo = m_TextComponent.textInfo;
                int characterCount = textInfo.characterCount;
                if (characterCount == 0) continue;

                for (int i = 0; i < characterCount; i++)
                {
                    if (!textInfo.characterInfo[i].isVisible)
                        continue;

                    int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                    // Get the index of the mesh used by this character.
                    int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                    vertices = textInfo.meshInfo[materialIndex].vertices;

                    // Compute the baseline mid point for each character
                    Vector3 offsetToMidBaseline = new Vector2((vertices[vertexIndex + 0].x + vertices[vertexIndex + 2].x) / 2, textInfo.characterInfo[i].baseLine);
                    //float offsetY = VertexCurve.Evaluate((float)i / characterCount + loopCount / 50f); // Random.Range(-0.25f, 0.25f);

                    // Compute the angle of rotation for each character based on the animation curve
                    var time01 = GetCurveTime(offsetToMidBaseline.x);
                    var point =  GetPointAtCurve(GetCurveTime(time01));
                    var rot = Quaternion.LookRotation(m_TextComponent.transform.forward, GetNormalAtCurve(time01));
                    matrix = Matrix4x4.TRS(point, rot, Vector3.one);

                    vertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 0]);
                    vertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 1]);
                    vertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 2]);
                    vertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 3]);
                }


                // Upload the mesh with the revised information
                m_TextComponent.UpdateVertexData();

                yield return new WaitForSeconds(0.025f);
            }
        }
    }
}
