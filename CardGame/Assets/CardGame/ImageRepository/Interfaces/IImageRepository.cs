using System.Threading.Tasks;
using UnityEngine;

namespace CardGame.ImageRepository
{
    public interface IImageRepository
    {
        Task<Sprite> Get();

        Task<bool> Connection();
    }
}