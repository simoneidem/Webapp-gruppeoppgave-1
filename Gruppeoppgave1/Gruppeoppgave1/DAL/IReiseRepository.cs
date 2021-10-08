using Gruppeoppgave1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL
{
    public interface IReiseRepository
    {
        Task<bool> Bestille(Reise innReis);

        Task<List<Reise>> HentAlle();

        Task<bool> Slett(int id);

        Task<Reise> HentEn(int id);

        Task<bool> Endre(Reise endreReise);
    }
}
