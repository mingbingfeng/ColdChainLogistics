using System;

namespace C2LP.Assistant.TMSConsole.Model.TMSOrder.MESSAGEDETAIL
{
    [Serializable]
    public class DETAIL
    {
        public string TMSORDERDTLID { get; set; }

        public string BMSORDERDTLID { get; set; }

        public string SALESNO { get; set; }
        public string BACODE { get; set; }
        public string GOODSOWNID { get; set; }
        public string GOODSCODE { get; set; }
        public string GOODSNAME { get; set; }
        public string GOODSTYPE { get; set; }
        public string DRUGTYPE { get; set; }
        public string LOTNO { get; set; }
        public string PRODDATE { get; set; }
        public string APPROVENO { get; set; }
        public string TRADEPACK { get; set; }
        public string PACKAGEUNIT { get; set; }
        public string PACKAGEQTY { get; set; }
        public string QUNTITY { get; set; }
        public string FACTQUNTITY { get; set; }
        public string WHOLEQUNTITY { get; set; }
        public string SINGLEQUNTITY { get; set; }
        public string WEIGHT { get; set; }
        public string VOLUME { get; set; }
        public string VALIDDATE { get; set; }
        public string PINBACK { get; set; }
        public string WORKREQUIRE { get; set; }
        public string EXTCOL0 { get; set; }
    }
}
