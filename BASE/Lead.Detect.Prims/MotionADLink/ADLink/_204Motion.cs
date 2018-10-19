using System.Windows.Forms;

namespace Lead.Detect.PrimMotionADLink.ADLink
{
    public class Adlink204C
    {
        public int CardId;
        public int CardName;
        public int StartAxisId;
        public int TotalAxis;

        public bool IsInitialed { get; private set; }
        public bool IsLoadXmlFile { get; private set; }


        public void Initial(int cardId, int mode)
        {
            CardId = cardId;
            var boardIdInBits = 0;
            // Card(Board) initial,mode bit0(0:By system assigned, 1:By dip switch)  
            var ret = APS168.APS_initial(ref boardIdInBits, mode);
            if (ret >= 0)
            {
                IsInitialed = true;
                APS168.APS_get_first_axisId(cardId, ref StartAxisId, ref TotalAxis);
                APS168.APS_get_card_name(cardId, ref CardName);
                if (CardName != (int) APS_Define.DEVICE_NAME_PCI_825458 && CardName != (int) APS_Define.DEVICE_NAME_AMP_20408C) MessageBox.Show("运动控制是型号不是204C或208C！");
            }
            else
            {
                MessageBox.Show("运动控制卡初始化失败，请检查驱动是否装好或者MotionCreatePro已经开启！");
            }
        }

        public bool LoadParamFromFile(string xmlfilename)
        {
            IsLoadXmlFile = APS168.APS_load_param_from_file(xmlfilename) == 0;
            if (IsLoadXmlFile)
                AutoClosingMessageBox.Show("Load XML File OK !", "204C208C", 2000);
            else
                MessageBox.Show("Load XML File Failed!");
            return IsLoadXmlFile;
        }
    }
}