using System;
using Xunit;
using Moq;
using Gruppeoppgave1.Controllers;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;




namespace xUnitTestProject1
{
    public class ReiseTest
    {

        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<IReiseRepository> mockRep = new Mock<IReiseRepository>();
        private readonly Mock<ILogger<ReiseController>> mockLog = new Mock<ILogger<ReiseController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();


        ///////////////// Bestille tester /////////////////
        [Fact]
        public async Task BestilleLoggetInnOk()
        {
            
            mockRep.Setup(k => k.Bestille(It.IsAny<Reise>())).ReturnsAsync(true);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.Bestille(It.IsAny<Reise>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Reise lagret", resultat.Value);
        }

        [Fact]
        public async Task BestilleLoggetInnIkkeOk()
        {
            mockRep.Setup(k => k.Bestille(It.IsAny<Reise>())).ReturnsAsync(false);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.Bestille(It.IsAny<Reise>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Reisen kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task BestilleLoggetInnFeilModel()
        {
            var reise1 = new Reise
            {
                Id = 1,
                Type = "En vei",
                Strekning = "Oslo - Kiel",
                Dato = "09-10-12",
                Innreise = "10-10-12",
                Tid = "14:00",
                Reiseid = "C9543",
                Pris = 600,
                BillettId = 1,
                Voksen = 2,
                Barn = 2,
                honnor = 0,
                Student = 0,
                TransportId = 1,
                Bil = 1,
                Motorsykkel = 0,
                Sykkel = 0
            };

            mockRep.Setup(k => k.Bestille(reise1)).ReturnsAsync(true);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            reiseController.ModelState.AddModelError("Type", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.Bestille(reise1) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task BestilleIkkeLoggetInn()
        {
            mockRep.Setup(k => k.Bestille(It.IsAny<Reise>())).ReturnsAsync(true);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.Bestille(It.IsAny<Reise>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        ///////////////// HentAlle tester /////////////////
        [Fact]
        public async Task HentAlleLoggetInnOk()
        {
            var reise1 = new Reise
            {
                Id = 1,
                Type = "En vei",
                Strekning = "Oslo - Kiel",
                Dato = "09-10-12",
                Innreise = "10-10-12",
                Tid = "14:00",
                Reiseid = "C9543",
                Pris = 600,
                BillettId = 1,
                Voksen = 2,
                Barn = 2,
                honnor = 0,
                Student = 0,
                TransportId = 1,
                Bil = 1,
                Motorsykkel = 0,
                Sykkel = 0
            };
            var reise2 = new Reise
            {
                Id = 2,
                Type = "En vei",
                Strekning = "Oslo - Kiel",
                Dato = "09-10-12",
                Innreise = "10-10-12",
                Tid = "12:00",
                Reiseid = "C9543",
                Pris = 600,
                BillettId = 2,
                Voksen = 2,
                Barn = 2,
                honnor = 2,
                Student = 2,
                TransportId = 2,
                Bil = 2,
                Motorsykkel = 2,
                Sykkel = 2
            };
            var reise3 = new Reise
            {
                Id = 3,
                Type = "En vei",
                Strekning = "Oslo - Kiel",
                Dato = "09-10-12",
                Innreise = "10-10-12",
                Tid = "13:00",
                Reiseid = "C9543",
                Pris = 1000,
                BillettId = 3,
                Voksen = 2,
                Barn = 0,
                honnor = 0,
                Student = 0,
                TransportId = 3,
                Bil = 2,
                Motorsykkel = 0,
                Sykkel = 0
            };

            var reiseliste = new List<Reise>();
            reiseliste.Add(reise1);
            reiseliste.Add(reise2);
            reiseliste.Add(reise3);

            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(reiseliste);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.HentAlle() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Reise>>((List<Reise>)resultat.Value, reiseliste);
        }

        [Fact]
        public async Task HentAlleIkkeLoggetInn()
        {
            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(It.IsAny<List<Reise>>());

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.HentAlle() as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        ///////////////// HentEn tester /////////////////
        [Fact]
        public async Task HentEnLoggetInnOk()
        {
            var returReise = new Reise
            {
                Id = 1,
                Type = "En vei",
                Strekning = "Oslo - Kiel",
                Dato = "09-10-12",
                Innreise = "10-10-12",
                Tid = "14:00",
                Reiseid = "C9543",
                Pris = 600,
                BillettId = 1,
                Voksen = 2,
                Barn = 2,
                honnor = 0,
                Student = 0,
                TransportId = 1,
                Bil = 1,
                Motorsykkel = 0,
                Sykkel = 0
            };

            mockRep.Setup(k => k.HentEn(It.IsAny<int>())).ReturnsAsync(returReise);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.HentEn(It.IsAny<int>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Reise>(returReise, (Reise)resultat.Value);
        }

        [Fact]
        public async Task HentEnLoggetInnIkkeOk()
        {
            mockRep.Setup(k => k.HentEn(It.IsAny<int>())).ReturnsAsync(() => null); // merk denne null setting!

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.HentEn(It.IsAny<int>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke reisen", resultat.Value);
        }

        [Fact]
        public async Task HentEnIkkeLoggetInn()
        {
            mockRep.Setup(k => k.HentEn(It.IsAny<int>())).ReturnsAsync(() => null);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.HentEn(It.IsAny<int>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        ///////////////// Slett tester /////////////////
        [Fact]
        public async Task SlettLoggetInnOk()
        {
            mockRep.Setup(k => k.Slett(It.IsAny<int>())).ReturnsAsync(true);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.Slett(It.IsAny<int>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Reise slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettLoggetInnIkkeOk()
        {
            mockRep.Setup(k => k.Slett(It.IsAny<int>())).ReturnsAsync(false);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.Slett(It.IsAny<int>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting av Reisen ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task SlettLoggetIkkeLoggetInn()
        {
            mockRep.Setup(k => k.Slett(It.IsAny<int>())).ReturnsAsync(true);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.Slett(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        ///////////////// Endre tester /////////////////
        [Fact]
        public async Task EndreLoggetInnOk()
        {
            mockRep.Setup(k => k.Endre(It.IsAny<Reise>())).ReturnsAsync(true);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.Endre(It.IsAny<Reise>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Reise endret", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnIkkeOk()
        {
            mockRep.Setup(k => k.Bestille(It.IsAny<Reise>())).ReturnsAsync(false);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.Endre(It.IsAny<Reise>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen av reisen kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnFeilModel()
        {
            var reise1 = new Reise
            {
                Id = 1,
                Type = "En vei",
                Strekning = "Oslo - Kiel",
                Dato = "09-10-12",
                Innreise = "10-10-12",
                Tid = "14:00",
                Reiseid = "C9543",
                Pris = 600,
                BillettId = 1,
                Voksen = 2,
                Barn = 2,
                honnor = 0,
                Student = 0,
                TransportId = 1,
                Bil = 1,
                Motorsykkel = 0,
                Sykkel = 0
            };

            mockRep.Setup(k => k.Endre(reise1)).ReturnsAsync(true);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            reiseController.ModelState.AddModelError("Type", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.Endre(reise1) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreIkkeLoggetInn()
        {
            mockRep.Setup(k => k.Endre(It.IsAny<Reise>())).ReturnsAsync(true);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.Endre(It.IsAny<Reise>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        ///////////////// LoggInn tester /////////////////
        [Fact]
        public async Task LoggInnOK()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnFeilPassordEllerBruker()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(false);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnInputFeil()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);

            reiseController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await reiseController.LoggInn(It.IsAny<Bruker>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public void LoggUt()
        {
            var reiseController = new ReiseController(mockRep.Object, mockLog.Object);
            
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            reiseController.LoggUt();

            Assert.Equal(_ikkeLoggetInn,mockSession[_loggetInn]);
        }
    }
}
