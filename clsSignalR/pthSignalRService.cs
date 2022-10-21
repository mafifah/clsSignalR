using Microsoft.AspNetCore.SignalR.Client;

namespace clsSignalR
{
    public class pthSignalRService
    {
        private HubConnection _hc { get; set; }
        private string _divisi { get; set; }
        private string _klien { get; set; }

        public pthSignalRService()
        {
            //HttpClientHandler HttpCh = new HttpClientHandler();
            //HttpCh.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://assignalrserver.azurewebsites.net/chathub");
            _hc = new HubConnectionBuilder().WithUrl(client.BaseAddress).Build();
            //_hc.On<Action<PesanSignalR>, bool>("TerimaPesan", TerimaPesan);
        }

        public async Task MulaiKoneksi(string klien)
        {
            _klien = klien;
            if (_hc.ConnectionId != null)
            {
                await Disconnect();
            }
            await _hc.StartAsync();
            await _hc.InvokeAsync("MulaiKoneksi", _klien);
        }

        public async Task Disconnect()
        {
            try
            {
                await _hc.InvokeAsync("StopKoneksi", _klien);
                await _hc.StopAsync();
            }
            catch (Exception e)
            {
                var msg = e.Message;
            }
        }

        public async Task KirimPesan(long IdForm, string NamaForm, string StatusAction, string NamaFieldPK, object NilaiPK, DateTimeOffset WaktuProses, string IsiPesan, string platform)
        {
            var msg = $"{platform}_{_klien}";
            var psr = new PesanSignalR
            {
                Klien = _klien,
                Divisi = "",
                IdUser = "",
                IdSignal = Guid.NewGuid(),
                IdForm = IdForm,
                NamaForm = NamaForm,
                StatusAction = StatusAction,
                NamaFieldPK = NamaFieldPK,
                NilaiPK = NilaiPK,
                WaktuProses = WaktuProses,
                IsiPesan = IsiPesan,
                JenisPesan = msg
            };
            try
            {
                if (_hc.State == HubConnectionState.Connected)
                {
                    await _hc.InvokeAsync("KirimPesan", psr);
                }
            }
            catch (Exception ex) {
                var x = ex.Message;
            }
        }

        public void TerimaPesan(Action<PesanSignalR> a_psr = null, bool isBroadcast = false)
        {
            if (isBroadcast)
            {
                _hc.On("KirimPesanBroadcast", a_psr);
            }
            else
            {
                _hc.On("KirimPesan", a_psr);
            }
        }
    }
}
