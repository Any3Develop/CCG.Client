using System.Threading.Tasks;
using UnityEngine;

namespace Core.ArtRepository
{
    public interface IArtRepository
    {
        Task<Texture> GetAsync(string id);
    }
}