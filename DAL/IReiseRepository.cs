
using weboppg1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weboppg1.DAL
{
    public interface IReiseRepository
    {
        Task<bool> Bestille(Reise innReise);

        Task<List<Reise>> HentAlle();

        Task<bool> Slett(int id);

        Task<Reise> HentEn(int id);

        Task<bool> Endre(Reise endreReise);
    }
}
