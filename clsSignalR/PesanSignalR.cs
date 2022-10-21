namespace clsSignalR
{
    public class PesanSignalR
    {
        public string Klien { get; set; }
        public string Divisi { get; set; }
        public object IdUser { get; set; }
        public Guid IdSignal { get; set; }
        public long IdForm { get; set; }
        public string NamaForm { get; set; }
        public string StatusAction { get; set; }
        public string NamaFieldPK { get; set; }
        public object NilaiPK { get; set; }
        public DateTimeOffset WaktuProses { get; set; }
        public string IsiPesan { get; set; }
        public string JenisPesan { get; set; }

        public string NamaFolderThumbnail { get; set; }
        public string NamaFileThumbnail { get; set; }
    }
}
