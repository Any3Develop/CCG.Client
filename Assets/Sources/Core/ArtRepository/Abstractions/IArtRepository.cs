using System.Threading.Tasks;
using UnityEngine;

namespace Core.Network.ArtRepository
{
    public interface IArtRepository
    {
        Task<Texture> GetAsync(string id);
    }
}