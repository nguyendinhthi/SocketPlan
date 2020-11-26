using System;
using System.Collections.Generic;
using System.Text;
using SocketPlan.WinUI;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public class EquipmentSelectionDialog : EquipmentSelectForm
    {
        private static EquipmentSelectionDialog instance;
        public static new EquipmentSelectionDialog Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new EquipmentSelectionDialog();

                return instance;
            }
        }

        public static Equipment SelectedEquipment { get; set; }

        private EquipmentSelectionDialog()
            : base()
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SelectedEquipment = null;
        }

        protected override void OnSelectEquipment()
        {
            try
            {
                var items = this.equipmentListView.SelectedItems;
                if (items.Count == 0)
                    return;

                SelectedEquipment = items[0].Tag as Equipment;
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
        }

        protected override void OnSelectRelatedEquipment()
        {
            try
            {
                var items = this.relatedEquipmentListView.SelectedItems;
                if (items.Count == 0)
                    return;

                SelectedEquipment = items[0].Tag as Equipment;
                this.Hide();
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem287 = new System.Windows.Forms.ListViewItem("Ido Senyou Bousui Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水専-08.GIF");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EquipmentSelectionDialog));
            System.Windows.Forms.ListViewItem listViewItem288 = new System.Windows.Forms.ListViewItem("Horigotatsu senyou enashi", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無-02b.GIF");
            System.Windows.Forms.ListViewItem listViewItem289 = new System.Windows.Forms.ListViewItem("Horigotatsu you", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-03a.GIF");
            System.Windows.Forms.ListViewItem listViewItem290 = new System.Windows.Forms.ListViewItem("Normal socket", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-01.gif");
            System.Windows.Forms.ListViewItem listViewItem291 = new System.Windows.Forms.ListViewItem("AC Senyou Etsuki (Aircon socket)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\ｴｱｺﾝ-01.gif");
            System.Windows.Forms.ListViewItem listViewItem292 = new System.Windows.Forms.ListViewItem("WASHING MACHINE SOCKET", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-04.png");
            System.Windows.Forms.ListViewItem listViewItem293 = new System.Windows.Forms.ListViewItem("REF SOCKET", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-07.png");
            System.Windows.Forms.ListViewItem listViewItem294 = new System.Windows.Forms.ListViewItem("Purojekuta you", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-10.gif");
            System.Windows.Forms.ListViewItem listViewItem295 = new System.Windows.Forms.ListViewItem("Sukuri-n you chokketsu", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-20.gif");
            System.Windows.Forms.ListViewItem listViewItem296 = new System.Windows.Forms.ListViewItem("Anpu you", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-11.gif");
            System.Windows.Forms.ListViewItem listViewItem297 = new System.Windows.Forms.ListViewItem("JCL", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\JCL.gif");
            System.Windows.Forms.ListViewItem listViewItem298 = new System.Windows.Forms.ListViewItem("JC", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\JC.gif");
            System.Windows.Forms.ListViewItem listViewItem299 = new System.Windows.Forms.ListViewItem("JCT", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\JCT.GIF");
            System.Windows.Forms.ListViewItem listViewItem300 = new System.Windows.Forms.ListViewItem("RAY AC Senyou Etsuki 200V", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\ｴｱｺﾝ-3.GIF");
            System.Windows.Forms.ListViewItem listViewItem301 = new System.Windows.Forms.ListViewItem("AC Socket with 200V", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\ｴｱｺﾝ-02.gif");
            System.Windows.Forms.ListViewItem listViewItem302 = new System.Windows.Forms.ListViewItem("AC Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\ｴｱｺﾝ-01(Blue).GIF");
            System.Windows.Forms.ListViewItem listViewItem303 = new System.Windows.Forms.ListViewItem("Horigotatsu  you", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-03.gif");
            System.Windows.Forms.ListViewItem listViewItem304 = new System.Windows.Forms.ListViewItem("Horigotatsu senyou enashi", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無-02.gif");
            System.Windows.Forms.ListViewItem listViewItem305 = new System.Windows.Forms.ListViewItem("ｻｳﾅ-01", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\ｻｳﾅ-01.gif");
            System.Windows.Forms.ListViewItem listViewItem306 = new System.Windows.Forms.ListViewItem("Etsuki Socket", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-01.gif");
            System.Windows.Forms.ListViewItem listViewItem307 = new System.Windows.Forms.ListViewItem("Etsuki socket (blue pattern)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-02.gif");
            System.Windows.Forms.ListViewItem listViewItem308 = new System.Windows.Forms.ListViewItem("Chokketsu Etsuki- SP", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-03.gif");
            System.Windows.Forms.ListViewItem listViewItem309 = new System.Windows.Forms.ListViewItem("Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-01.gif");
            System.Windows.Forms.ListViewItem listViewItem310 = new System.Windows.Forms.ListViewItem("Senyou Etsuki (blue pattern)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-02.gif");
            System.Windows.Forms.ListViewItem listViewItem311 = new System.Windows.Forms.ListViewItem("Security Alarm", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-26.gif");
            System.Windows.Forms.ListViewItem listViewItem312 = new System.Windows.Forms.ListViewItem("Rimokon HB chokketsu etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-15.gif");
            System.Windows.Forms.ListViewItem listViewItem313 = new System.Windows.Forms.ListViewItem("Original Solar socket (3power con)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-37.GIF");
            System.Windows.Forms.ListViewItem listViewItem314 = new System.Windows.Forms.ListViewItem("Original Solar socket(one power con)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-29.gif");
            System.Windows.Forms.ListViewItem listViewItem315 = new System.Windows.Forms.ListViewItem("Original Solar socket(two power con)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-30.gif");
            System.Windows.Forms.ListViewItem listViewItem316 = new System.Windows.Forms.ListViewItem("Denshi Renji Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\電子ﾚﾝｼﾞ.gif");
            System.Windows.Forms.ListViewItem listViewItem317 = new System.Windows.Forms.ListViewItem("Dendou mizunukisen you", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-25.gif");
            System.Windows.Forms.ListViewItem listViewItem318 = new System.Windows.Forms.ListViewItem("Jeth Bath", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-13.gif");
            System.Windows.Forms.ListViewItem listViewItem319 = new System.Windows.Forms.ListViewItem("Hoshihimesama you chokketsu", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-24.gif");
            System.Windows.Forms.ListViewItem listViewItem320 = new System.Windows.Forms.ListViewItem("Kamidana you", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-04.gif");
            System.Windows.Forms.ListViewItem listViewItem321 = new System.Windows.Forms.ListViewItem("Kitchen Tsurido", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-05.gif");
            System.Windows.Forms.ListViewItem listViewItem322 = new System.Windows.Forms.ListViewItem("Gasu more you", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-09.gif");
            System.Windows.Forms.ListViewItem listViewItem323 = new System.Windows.Forms.ListViewItem("Senyou Enashi H=1900 (for i-smart cupboard)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無-01.gif");
            System.Windows.Forms.ListViewItem listViewItem324 = new System.Windows.Forms.ListViewItem("Senyou Enashi (For I-smart open kitchen)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無-09.gif");
            System.Windows.Forms.ListViewItem listViewItem325 = new System.Windows.Forms.ListViewItem("Senyou Enashi W/O Height (For I-smart cupboard)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無-01.gif");
            System.Windows.Forms.ListViewItem listViewItem326 = new System.Windows.Forms.ListViewItem("exclusive socket without ground", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無(red).GIF");
            System.Windows.Forms.ListViewItem listViewItem327 = new System.Windows.Forms.ListViewItem("IH Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-11.gif");
            System.Windows.Forms.ListViewItem listViewItem328 = new System.Windows.Forms.ListViewItem("Shokusenki Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\食洗機.gif");
            System.Windows.Forms.ListViewItem listViewItem329 = new System.Windows.Forms.ListViewItem("Kankisen you (wall type)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-12.gif");
            System.Windows.Forms.ListViewItem listViewItem330 = new System.Windows.Forms.ListViewItem("Kankisen you etsuki (wall type)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-09.gif");
            System.Windows.Forms.ListViewItem listViewItem331 = new System.Windows.Forms.ListViewItem("Kankisen you chokketsu - SP", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-23.gif");
            System.Windows.Forms.ListViewItem listViewItem332 = new System.Windows.Forms.ListViewItem("家電収納2", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\家電収納2.gif");
            System.Windows.Forms.ListViewItem listViewItem333 = new System.Windows.Forms.ListViewItem("Touchless faucet", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-22.gif");
            System.Windows.Forms.ListViewItem listViewItem334 = new System.Windows.Forms.ListViewItem("Kitchen hatch", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-07.gif");
            System.Windows.Forms.ListViewItem listViewItem335 = new System.Windows.Forms.ListViewItem("Open kitchen socket with yokotsuke", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-08.gif");
            System.Windows.Forms.ListViewItem listViewItem336 = new System.Windows.Forms.ListViewItem("Gasu O-bun Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-04.gif");
            System.Windows.Forms.ListViewItem listViewItem337 = new System.Windows.Forms.ListViewItem("Gasu O-bun Renji Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-05.gif");
            System.Windows.Forms.ListViewItem listViewItem338 = new System.Windows.Forms.ListViewItem("Denki Onsuiki Senyou Etsuki Chokketsu 200V", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-15.gif");
            System.Windows.Forms.ListViewItem listViewItem339 = new System.Windows.Forms.ListViewItem("Senyou bousui chokketsu w/ 200V", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水専-03.gif");
            System.Windows.Forms.ListViewItem listViewItem340 = new System.Windows.Forms.ListViewItem("Denki Onsuiki Senyou Etsuki Shoraiteki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-16.gif");
            System.Windows.Forms.ListViewItem listViewItem341 = new System.Windows.Forms.ListViewItem("Denki Onsuiki You", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-15.gif");
            System.Windows.Forms.ListViewItem listViewItem342 = new System.Windows.Forms.ListViewItem("Denki O-bun Senyou Etsuki 200V", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-18.gif");
            System.Windows.Forms.ListViewItem listViewItem343 = new System.Windows.Forms.ListViewItem("Denki O-bun Renji Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-19.gif");
            System.Windows.Forms.ListViewItem listViewItem344 = new System.Windows.Forms.ListViewItem("Disposer socket", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-17.GIF");
            System.Windows.Forms.ListViewItem listViewItem345 = new System.Windows.Forms.ListViewItem("Bousui Etsuki with Futa kuchi", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水E付-01.gif");
            System.Windows.Forms.ListViewItem listViewItem346 = new System.Windows.Forms.ListViewItem("Bousui Etsuki with San Kuchi", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水E付-06.GIF");
            System.Windows.Forms.ListViewItem listViewItem347 = new System.Windows.Forms.ListViewItem("bousui Etsuki for outside disposer", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水E付-05.GIF");
            System.Windows.Forms.ListViewItem listViewItem348 = new System.Windows.Forms.ListViewItem("Bousui Etsuki Senyou", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水専-01.gif");
            System.Windows.Forms.ListViewItem listViewItem349 = new System.Windows.Forms.ListViewItem("Chokketsu Senyou Bousui Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水専-02.gif");
            System.Windows.Forms.ListViewItem listViewItem350 = new System.Windows.Forms.ListViewItem("Denki Onsuiki Senyou Bousui", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水専-05.gif");
            System.Windows.Forms.ListViewItem listViewItem351 = new System.Windows.Forms.ListViewItem("bousui socket", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水ｺﾝｾﾝﾄ 2.GIF");
            System.Windows.Forms.ListViewItem listViewItem352 = new System.Windows.Forms.ListViewItem("senyou bousui socket", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用防水ｺﾝｾﾝﾄ.gif");
            System.Windows.Forms.ListViewItem listViewItem353 = new System.Windows.Forms.ListViewItem("Ekokyu-to Senyou Etsuki Chokketsu ", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-13.gif");
            System.Windows.Forms.ListViewItem listViewItem354 = new System.Windows.Forms.ListViewItem("Ekokyu-to Senyou Etsuki Shoraiteki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-17.gif");
            System.Windows.Forms.ListViewItem listViewItem355 = new System.Windows.Forms.ListViewItem("IH Senyou Etsuki Shoraiteki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-12.gif");
            System.Windows.Forms.ListViewItem listViewItem356 = new System.Windows.Forms.ListViewItem("foot-light thermal sensor type (New)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-33-.GIF");
            System.Windows.Forms.ListViewItem listViewItem357 = new System.Windows.Forms.ListViewItem("foot-light sensor type (NEW)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-34-.GIF");
            System.Windows.Forms.ListViewItem listViewItem358 = new System.Windows.Forms.ListViewItem("EV-PHEV Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\有り-.GIF");
            System.Windows.Forms.ListViewItem listViewItem359 = new System.Windows.Forms.ListViewItem("Derishia you with 100V", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-35.gif");
            System.Windows.Forms.ListViewItem listViewItem360 = new System.Windows.Forms.ListViewItem("Toire you", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-17.gif");
            System.Windows.Forms.ListViewItem listViewItem361 = new System.Windows.Forms.ListViewItem("Toire You Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-12.gif");
            System.Windows.Forms.ListViewItem listViewItem362 = new System.Windows.Forms.ListViewItem("Shawa toire you etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-06.gif");
            System.Windows.Forms.ListViewItem listViewItem363 = new System.Windows.Forms.ListViewItem("Apricot - Shawa toire senyou etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-03.gif");
            System.Windows.Forms.ListViewItem listViewItem364 = new System.Windows.Forms.ListViewItem("Tankuresu Toire You Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-21.gif");
            System.Windows.Forms.ListViewItem listViewItem365 = new System.Windows.Forms.ListViewItem("Senmendai Socket", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-13.gif");
            System.Windows.Forms.ListViewItem listViewItem366 = new System.Windows.Forms.ListViewItem("Senmendai Socket (for restroom dresser)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-06-new.GIF");
            System.Windows.Forms.ListViewItem listViewItem367 = new System.Windows.Forms.ListViewItem("Sentakuki Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-06.gif");
            System.Windows.Forms.ListViewItem listViewItem368 = new System.Windows.Forms.ListViewItem("Sentakuki kansouki senyou etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\洗濯乾燥機.gif");
            System.Windows.Forms.ListViewItem listViewItem369 = new System.Windows.Forms.ListViewItem("Kansouki Senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-07.gif");
            System.Windows.Forms.ListViewItem listViewItem370 = new System.Windows.Forms.ListViewItem("Boira Socket Bousui Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水E付-03.gif");
            System.Windows.Forms.ListViewItem listViewItem371 = new System.Windows.Forms.ListViewItem("Boira Socket Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-08.gif");
            System.Windows.Forms.ListViewItem listViewItem372 = new System.Windows.Forms.ListViewItem("Buroa Socket Bousui Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水E付-04.gif");
            System.Windows.Forms.ListViewItem listViewItem373 = new System.Windows.Forms.ListViewItem("Touketsu boushi bousui etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水E付-02.gif");
            System.Windows.Forms.ListViewItem listViewItem374 = new System.Windows.Forms.ListViewItem("FOR SHOUBENKI - Touketsu boushi etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-16.gif");
            System.Windows.Forms.ListViewItem listViewItem375 = new System.Windows.Forms.ListViewItem("Seisuiki you", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-21.gif");
            System.Windows.Forms.ListViewItem listViewItem376 = new System.Windows.Forms.ListViewItem("Ashimoto Onpuuki you", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-05.gif");
            System.Windows.Forms.ListViewItem listViewItem377 = new System.Windows.Forms.ListViewItem("Dendoushatta you chokketsu etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-10.gif");
            System.Windows.Forms.ListViewItem listViewItem378 = new System.Windows.Forms.ListViewItem("OLD Denki Rimokon key chokketsu etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-11.gif");
            System.Windows.Forms.ListViewItem listViewItem379 = new System.Windows.Forms.ListViewItem("Yunitto Shawa You Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\E付-14.gif");
            System.Windows.Forms.ListViewItem listViewItem380 = new System.Windows.Forms.ListViewItem("Chikunetsu Danbou", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-08.gif");
            System.Windows.Forms.ListViewItem listViewItem381 = new System.Windows.Forms.ListViewItem("Ekowiru senyou Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-22.gif");
            System.Windows.Forms.ListViewItem listViewItem382 = new System.Windows.Forms.ListViewItem("HTU Chokketsu Senyou Etsuki 200V", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-24.gif");
            System.Windows.Forms.ListViewItem listViewItem383 = new System.Windows.Forms.ListViewItem("Haisui ponpu senyou bousui", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水専-04.gif");
            System.Windows.Forms.ListViewItem listViewItem384 = new System.Windows.Forms.ListViewItem("Sekisui Jetto basu  senyou bousui etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水専-06.gif");
            System.Windows.Forms.ListViewItem listViewItem385 = new System.Windows.Forms.ListViewItem("O-ningu", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\オーニング用.gif");
            System.Windows.Forms.ListViewItem listViewItem386 = new System.Windows.Forms.ListViewItem("Chokketsu Senyou Enashi", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無-03.gif");
            System.Windows.Forms.ListViewItem listViewItem387 = new System.Windows.Forms.ListViewItem("Senyou Etsuki Haisen Toridashi", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-20.gif");
            System.Windows.Forms.ListViewItem listViewItem388 = new System.Windows.Forms.ListViewItem("Senyou Haisen Toridashi", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無-04.gif");
            System.Windows.Forms.ListViewItem listViewItem389 = new System.Windows.Forms.ListViewItem("Senyou Haisen Toridashix1", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無-05.gif");
            System.Windows.Forms.ListViewItem listViewItem390 = new System.Windows.Forms.ListViewItem("Senyou Haisen Toridashix2", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無-06.gif");
            System.Windows.Forms.ListViewItem listViewItem391 = new System.Windows.Forms.ListViewItem("Senyou Haisen Toridashix3", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無-07.gif");
            System.Windows.Forms.ListViewItem listViewItem392 = new System.Windows.Forms.ListViewItem("Senyou Haisen Toridashix4", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E無-08.gif");
            System.Windows.Forms.ListViewItem listViewItem393 = new System.Windows.Forms.ListViewItem("家電収納", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\家電収納.gif");
            System.Windows.Forms.ListViewItem listViewItem394 = new System.Windows.Forms.ListViewItem("蓄熱-4KW", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\蓄熱-4KW.gif");
            System.Windows.Forms.ListViewItem listViewItem395 = new System.Windows.Forms.ListViewItem("蓄熱-6KW", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\蓄熱-6KW.gif");
            System.Windows.Forms.ListViewItem listViewItem396 = new System.Windows.Forms.ListViewItem("蓄熱-7KW", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\蓄熱-7KW.gif");
            System.Windows.Forms.ListViewItem listViewItem397 = new System.Windows.Forms.ListViewItem("カッパボ01", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\カッパボ01.gif");
            System.Windows.Forms.ListViewItem listViewItem398 = new System.Windows.Forms.ListViewItem("Busuta Socket with Frame", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-02.gif");
            System.Windows.Forms.ListViewItem listViewItem399 = new System.Windows.Forms.ListViewItem("shower　socket", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\showersocket.GIF");
            System.Windows.Forms.ListViewItem listViewItem400 = new System.Windows.Forms.ListViewItem("Chokketsu -SP", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-32.gif");
            System.Windows.Forms.ListViewItem listViewItem401 = new System.Windows.Forms.ListViewItem("Senyou Etsuki Chokketsu for CV machine", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-10.gif");
            System.Windows.Forms.ListViewItem listViewItem402 = new System.Windows.Forms.ListViewItem("Senyou Etsuki Chokketsu", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-09.gif");
            System.Windows.Forms.ListViewItem listViewItem403 = new System.Windows.Forms.ListViewItem("Normal socket", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\normal socket(blue).GIF");
            System.Windows.Forms.ListViewItem listViewItem404 = new System.Windows.Forms.ListViewItem("Bousui Etsuki with Futa kuchi", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\Bousui(blue).GIF");
            System.Windows.Forms.ListViewItem listViewItem405 = new System.Windows.Forms.ListViewItem("Bousui Etsuki Senyou", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\Senyou Bousui(blue).GIF");
            System.Windows.Forms.ListViewItem listViewItem406 = new System.Windows.Forms.ListViewItem("Ekowiru blackout socket", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-38.GIF");
            System.Windows.Forms.ListViewItem listViewItem407 = new System.Windows.Forms.ListViewItem("DO NOT USE Safety light socket with sensor", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-40.GIF");
            System.Windows.Forms.ListViewItem listViewItem408 = new System.Windows.Forms.ListViewItem("Ekowiru chokketsu Senyou Etsuki (New)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-22-2.GIF");
            System.Windows.Forms.ListViewItem listViewItem409 = new System.Windows.Forms.ListViewItem("Bousui Etsuki with Hito Kuchi", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\防水E付-07.GIF");
            System.Windows.Forms.ListViewItem listViewItem410 = new System.Windows.Forms.ListViewItem("Senyou Bousui Etsuki with Hito Kuchi", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\senyou bousui.GIF");
            System.Windows.Forms.ListViewItem listViewItem411 = new System.Windows.Forms.ListViewItem("Senyou Bousui Etsuki with San Kuchi", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\senyou bousui etsuki.GIF");
            System.Windows.Forms.ListViewItem listViewItem412 = new System.Windows.Forms.ListViewItem("HCU chokketsu Senyou Etsuki 200V", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\専用E付-27.GIF");
            System.Windows.Forms.ListViewItem listViewItem413 = new System.Windows.Forms.ListViewItem("EHA Chokketsu  100V", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\Electric Honeycomb.GIF");
            System.Windows.Forms.ListViewItem listViewItem414 = new System.Windows.Forms.ListViewItem("EB/HU Chokketsu Etsuki 200V", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\Socket for EB.GIF");
            System.Windows.Forms.ListViewItem listViewItem415 = new System.Windows.Forms.ListViewItem("Chokketsu Senyou Etsuki (Kids Counter)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\Socket for kids counter kitchen.GIF");
            System.Windows.Forms.ListViewItem listViewItem416 = new System.Windows.Forms.ListViewItem("Nano Electric Chokketsu", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\Direct socket for NE.GIF");
            System.Windows.Forms.ListViewItem listViewItem417 = new System.Windows.Forms.ListViewItem("Gasu Obun Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\Gasu o-bun etsuki.GIF");
            System.Windows.Forms.ListViewItem listViewItem418 = new System.Windows.Forms.ListViewItem("Gasu Obun Renji Etsuki", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\Gasu obun renji etsuki.GIF");
            System.Windows.Forms.ListViewItem listViewItem419 = new System.Windows.Forms.ListViewItem("Ashimototou (LSBJ50002)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\ASHIMOTOTOU(LSBJ50002).GIF");
            System.Windows.Forms.ListViewItem listViewItem420 = new System.Windows.Forms.ListViewItem("Safety socket", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-40.GIF");
            System.Windows.Forms.ListViewItem listViewItem421 = new System.Windows.Forms.ListViewItem("Chokketsu kankisen(for ceiling vent)", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-48.gif");
            System.Windows.Forms.ListViewItem listViewItem422 = new System.Windows.Forms.ListViewItem("Aketara taima (main)- Standard", "C:\\UnitWiring\\Images\\Equipments\\スイッチ\\あけたらﾀｲﾏ-01.GIF");
            System.Windows.Forms.ListViewItem listViewItem423 = new System.Windows.Forms.ListViewItem("一般-50", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\一般-01.gif");
            System.Windows.Forms.ListViewItem listViewItem424 = new System.Windows.Forms.ListViewItem("Solar+WH (right)", "C:\\UnitWiring\\Images\\Equipments\\Elevation\\WH+Solar-08.gif");
            System.Windows.Forms.ListViewItem listViewItem425 = new System.Windows.Forms.ListViewItem("Solar+WH (down)", "C:\\UnitWiring\\Images\\Equipments\\Elevation\\WH+Solar-05.gif");
            System.Windows.Forms.ListViewItem listViewItem426 = new System.Windows.Forms.ListViewItem("JCT-New", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\JCT.GIF");
            System.Windows.Forms.ListViewItem listViewItem427 = new System.Windows.Forms.ListViewItem("Zenryou+WH (up)", "C:\\UnitWiring\\Images\\Equipments\\Elevation\\WH+Solar-03.gif");
            System.Windows.Forms.ListViewItem listViewItem428 = new System.Windows.Forms.ListViewItem("Blue arrow", "C:\\UnitWiring\\Images\\Equipments\\LIGHT PLAN\\BLUE ARROW.gif");
            System.Windows.Forms.ListViewItem listViewItem429 = new System.Windows.Forms.ListViewItem("専用E付-37", "C:\\UnitWiring\\Images\\Equipments\\コンセント\\Socket for kids counter kitchen.GIF");
            this.SuspendLayout();
            // 
            // equipmentListView
            // 
            listViewItem287.Tag = ((object)(resources.GetObject("listViewItem287.Tag")));
            listViewItem288.Tag = ((object)(resources.GetObject("listViewItem288.Tag")));
            listViewItem289.Tag = ((object)(resources.GetObject("listViewItem289.Tag")));
            listViewItem290.Tag = ((object)(resources.GetObject("listViewItem290.Tag")));
            listViewItem291.Tag = ((object)(resources.GetObject("listViewItem291.Tag")));
            listViewItem292.Tag = ((object)(resources.GetObject("listViewItem292.Tag")));
            listViewItem293.Tag = ((object)(resources.GetObject("listViewItem293.Tag")));
            listViewItem294.Tag = ((object)(resources.GetObject("listViewItem294.Tag")));
            listViewItem295.Tag = ((object)(resources.GetObject("listViewItem295.Tag")));
            listViewItem296.Tag = ((object)(resources.GetObject("listViewItem296.Tag")));
            listViewItem297.Tag = ((object)(resources.GetObject("listViewItem297.Tag")));
            listViewItem298.Tag = ((object)(resources.GetObject("listViewItem298.Tag")));
            listViewItem299.Tag = ((object)(resources.GetObject("listViewItem299.Tag")));
            listViewItem300.Tag = ((object)(resources.GetObject("listViewItem300.Tag")));
            listViewItem301.Tag = ((object)(resources.GetObject("listViewItem301.Tag")));
            listViewItem302.Tag = ((object)(resources.GetObject("listViewItem302.Tag")));
            listViewItem303.Tag = ((object)(resources.GetObject("listViewItem303.Tag")));
            listViewItem304.Tag = ((object)(resources.GetObject("listViewItem304.Tag")));
            listViewItem305.Tag = ((object)(resources.GetObject("listViewItem305.Tag")));
            listViewItem306.Tag = ((object)(resources.GetObject("listViewItem306.Tag")));
            listViewItem307.Tag = ((object)(resources.GetObject("listViewItem307.Tag")));
            listViewItem308.Tag = ((object)(resources.GetObject("listViewItem308.Tag")));
            listViewItem309.Tag = ((object)(resources.GetObject("listViewItem309.Tag")));
            listViewItem310.Tag = ((object)(resources.GetObject("listViewItem310.Tag")));
            listViewItem311.Tag = ((object)(resources.GetObject("listViewItem311.Tag")));
            listViewItem312.Tag = ((object)(resources.GetObject("listViewItem312.Tag")));
            listViewItem313.Tag = ((object)(resources.GetObject("listViewItem313.Tag")));
            listViewItem314.Tag = ((object)(resources.GetObject("listViewItem314.Tag")));
            listViewItem315.Tag = ((object)(resources.GetObject("listViewItem315.Tag")));
            listViewItem316.Tag = ((object)(resources.GetObject("listViewItem316.Tag")));
            listViewItem317.Tag = ((object)(resources.GetObject("listViewItem317.Tag")));
            listViewItem318.Tag = ((object)(resources.GetObject("listViewItem318.Tag")));
            listViewItem319.Tag = ((object)(resources.GetObject("listViewItem319.Tag")));
            listViewItem320.Tag = ((object)(resources.GetObject("listViewItem320.Tag")));
            listViewItem321.Tag = ((object)(resources.GetObject("listViewItem321.Tag")));
            listViewItem322.Tag = ((object)(resources.GetObject("listViewItem322.Tag")));
            listViewItem323.Tag = ((object)(resources.GetObject("listViewItem323.Tag")));
            listViewItem324.Tag = ((object)(resources.GetObject("listViewItem324.Tag")));
            listViewItem325.Tag = ((object)(resources.GetObject("listViewItem325.Tag")));
            listViewItem326.Tag = ((object)(resources.GetObject("listViewItem326.Tag")));
            listViewItem327.Tag = ((object)(resources.GetObject("listViewItem327.Tag")));
            listViewItem328.Tag = ((object)(resources.GetObject("listViewItem328.Tag")));
            listViewItem329.Tag = ((object)(resources.GetObject("listViewItem329.Tag")));
            listViewItem330.Tag = ((object)(resources.GetObject("listViewItem330.Tag")));
            listViewItem331.Tag = ((object)(resources.GetObject("listViewItem331.Tag")));
            listViewItem332.Tag = ((object)(resources.GetObject("listViewItem332.Tag")));
            listViewItem333.Tag = ((object)(resources.GetObject("listViewItem333.Tag")));
            listViewItem334.Tag = ((object)(resources.GetObject("listViewItem334.Tag")));
            listViewItem335.Tag = ((object)(resources.GetObject("listViewItem335.Tag")));
            listViewItem336.Tag = ((object)(resources.GetObject("listViewItem336.Tag")));
            listViewItem337.Tag = ((object)(resources.GetObject("listViewItem337.Tag")));
            listViewItem338.Tag = ((object)(resources.GetObject("listViewItem338.Tag")));
            listViewItem339.Tag = ((object)(resources.GetObject("listViewItem339.Tag")));
            listViewItem340.Tag = ((object)(resources.GetObject("listViewItem340.Tag")));
            listViewItem341.Tag = ((object)(resources.GetObject("listViewItem341.Tag")));
            listViewItem342.Tag = ((object)(resources.GetObject("listViewItem342.Tag")));
            listViewItem343.Tag = ((object)(resources.GetObject("listViewItem343.Tag")));
            listViewItem344.Tag = ((object)(resources.GetObject("listViewItem344.Tag")));
            listViewItem345.Tag = ((object)(resources.GetObject("listViewItem345.Tag")));
            listViewItem346.Tag = ((object)(resources.GetObject("listViewItem346.Tag")));
            listViewItem347.Tag = ((object)(resources.GetObject("listViewItem347.Tag")));
            listViewItem348.Tag = ((object)(resources.GetObject("listViewItem348.Tag")));
            listViewItem349.Tag = ((object)(resources.GetObject("listViewItem349.Tag")));
            listViewItem350.Tag = ((object)(resources.GetObject("listViewItem350.Tag")));
            listViewItem351.Tag = ((object)(resources.GetObject("listViewItem351.Tag")));
            listViewItem352.Tag = ((object)(resources.GetObject("listViewItem352.Tag")));
            listViewItem353.Tag = ((object)(resources.GetObject("listViewItem353.Tag")));
            listViewItem354.Tag = ((object)(resources.GetObject("listViewItem354.Tag")));
            listViewItem355.Tag = ((object)(resources.GetObject("listViewItem355.Tag")));
            listViewItem356.Tag = ((object)(resources.GetObject("listViewItem356.Tag")));
            listViewItem357.Tag = ((object)(resources.GetObject("listViewItem357.Tag")));
            listViewItem358.Tag = ((object)(resources.GetObject("listViewItem358.Tag")));
            listViewItem359.Tag = ((object)(resources.GetObject("listViewItem359.Tag")));
            listViewItem360.Tag = ((object)(resources.GetObject("listViewItem360.Tag")));
            listViewItem361.Tag = ((object)(resources.GetObject("listViewItem361.Tag")));
            listViewItem362.Tag = ((object)(resources.GetObject("listViewItem362.Tag")));
            listViewItem363.Tag = ((object)(resources.GetObject("listViewItem363.Tag")));
            listViewItem364.Tag = ((object)(resources.GetObject("listViewItem364.Tag")));
            listViewItem365.Tag = ((object)(resources.GetObject("listViewItem365.Tag")));
            listViewItem366.Tag = ((object)(resources.GetObject("listViewItem366.Tag")));
            listViewItem367.Tag = ((object)(resources.GetObject("listViewItem367.Tag")));
            listViewItem368.Tag = ((object)(resources.GetObject("listViewItem368.Tag")));
            listViewItem369.Tag = ((object)(resources.GetObject("listViewItem369.Tag")));
            listViewItem370.Tag = ((object)(resources.GetObject("listViewItem370.Tag")));
            listViewItem371.Tag = ((object)(resources.GetObject("listViewItem371.Tag")));
            listViewItem372.Tag = ((object)(resources.GetObject("listViewItem372.Tag")));
            listViewItem373.Tag = ((object)(resources.GetObject("listViewItem373.Tag")));
            listViewItem374.Tag = ((object)(resources.GetObject("listViewItem374.Tag")));
            listViewItem375.Tag = ((object)(resources.GetObject("listViewItem375.Tag")));
            listViewItem376.Tag = ((object)(resources.GetObject("listViewItem376.Tag")));
            listViewItem377.Tag = ((object)(resources.GetObject("listViewItem377.Tag")));
            listViewItem378.Tag = ((object)(resources.GetObject("listViewItem378.Tag")));
            listViewItem379.Tag = ((object)(resources.GetObject("listViewItem379.Tag")));
            listViewItem380.Tag = ((object)(resources.GetObject("listViewItem380.Tag")));
            listViewItem381.Tag = ((object)(resources.GetObject("listViewItem381.Tag")));
            listViewItem382.Tag = ((object)(resources.GetObject("listViewItem382.Tag")));
            listViewItem383.Tag = ((object)(resources.GetObject("listViewItem383.Tag")));
            listViewItem384.Tag = ((object)(resources.GetObject("listViewItem384.Tag")));
            listViewItem385.Tag = ((object)(resources.GetObject("listViewItem385.Tag")));
            listViewItem386.Tag = ((object)(resources.GetObject("listViewItem386.Tag")));
            listViewItem387.Tag = ((object)(resources.GetObject("listViewItem387.Tag")));
            listViewItem388.Tag = ((object)(resources.GetObject("listViewItem388.Tag")));
            listViewItem389.Tag = ((object)(resources.GetObject("listViewItem389.Tag")));
            listViewItem390.Tag = ((object)(resources.GetObject("listViewItem390.Tag")));
            listViewItem391.Tag = ((object)(resources.GetObject("listViewItem391.Tag")));
            listViewItem392.Tag = ((object)(resources.GetObject("listViewItem392.Tag")));
            listViewItem393.Tag = ((object)(resources.GetObject("listViewItem393.Tag")));
            listViewItem394.Tag = ((object)(resources.GetObject("listViewItem394.Tag")));
            listViewItem395.Tag = ((object)(resources.GetObject("listViewItem395.Tag")));
            listViewItem396.Tag = ((object)(resources.GetObject("listViewItem396.Tag")));
            listViewItem397.Tag = ((object)(resources.GetObject("listViewItem397.Tag")));
            listViewItem398.Tag = ((object)(resources.GetObject("listViewItem398.Tag")));
            listViewItem399.Tag = ((object)(resources.GetObject("listViewItem399.Tag")));
            listViewItem400.Tag = ((object)(resources.GetObject("listViewItem400.Tag")));
            listViewItem401.Tag = ((object)(resources.GetObject("listViewItem401.Tag")));
            listViewItem402.Tag = ((object)(resources.GetObject("listViewItem402.Tag")));
            listViewItem403.Tag = ((object)(resources.GetObject("listViewItem403.Tag")));
            listViewItem404.Tag = ((object)(resources.GetObject("listViewItem404.Tag")));
            listViewItem405.Tag = ((object)(resources.GetObject("listViewItem405.Tag")));
            listViewItem406.Tag = ((object)(resources.GetObject("listViewItem406.Tag")));
            listViewItem407.Tag = ((object)(resources.GetObject("listViewItem407.Tag")));
            listViewItem408.Tag = ((object)(resources.GetObject("listViewItem408.Tag")));
            listViewItem409.Tag = ((object)(resources.GetObject("listViewItem409.Tag")));
            listViewItem410.Tag = ((object)(resources.GetObject("listViewItem410.Tag")));
            listViewItem411.Tag = ((object)(resources.GetObject("listViewItem411.Tag")));
            listViewItem412.Tag = ((object)(resources.GetObject("listViewItem412.Tag")));
            listViewItem413.Tag = ((object)(resources.GetObject("listViewItem413.Tag")));
            listViewItem414.Tag = ((object)(resources.GetObject("listViewItem414.Tag")));
            listViewItem415.Tag = ((object)(resources.GetObject("listViewItem415.Tag")));
            listViewItem416.Tag = ((object)(resources.GetObject("listViewItem416.Tag")));
            listViewItem417.Tag = ((object)(resources.GetObject("listViewItem417.Tag")));
            listViewItem418.Tag = ((object)(resources.GetObject("listViewItem418.Tag")));
            listViewItem419.Tag = ((object)(resources.GetObject("listViewItem419.Tag")));
            listViewItem420.Tag = ((object)(resources.GetObject("listViewItem420.Tag")));
            listViewItem421.Tag = ((object)(resources.GetObject("listViewItem421.Tag")));
            listViewItem422.Tag = ((object)(resources.GetObject("listViewItem422.Tag")));
            listViewItem423.Tag = ((object)(resources.GetObject("listViewItem423.Tag")));
            listViewItem424.Tag = ((object)(resources.GetObject("listViewItem424.Tag")));
            listViewItem425.Tag = ((object)(resources.GetObject("listViewItem425.Tag")));
            listViewItem426.Tag = ((object)(resources.GetObject("listViewItem426.Tag")));
            listViewItem427.Tag = ((object)(resources.GetObject("listViewItem427.Tag")));
            listViewItem428.Tag = ((object)(resources.GetObject("listViewItem428.Tag")));
            listViewItem429.Tag = ((object)(resources.GetObject("listViewItem429.Tag")));
            this.equipmentListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem287,
            listViewItem288,
            listViewItem289,
            listViewItem290,
            listViewItem291,
            listViewItem292,
            listViewItem293,
            listViewItem294,
            listViewItem295,
            listViewItem296,
            listViewItem297,
            listViewItem298,
            listViewItem299,
            listViewItem300,
            listViewItem301,
            listViewItem302,
            listViewItem303,
            listViewItem304,
            listViewItem305,
            listViewItem306,
            listViewItem307,
            listViewItem308,
            listViewItem309,
            listViewItem310,
            listViewItem311,
            listViewItem312,
            listViewItem313,
            listViewItem314,
            listViewItem315,
            listViewItem316,
            listViewItem317,
            listViewItem318,
            listViewItem319,
            listViewItem320,
            listViewItem321,
            listViewItem322,
            listViewItem323,
            listViewItem324,
            listViewItem325,
            listViewItem326,
            listViewItem327,
            listViewItem328,
            listViewItem329,
            listViewItem330,
            listViewItem331,
            listViewItem332,
            listViewItem333,
            listViewItem334,
            listViewItem335,
            listViewItem336,
            listViewItem337,
            listViewItem338,
            listViewItem339,
            listViewItem340,
            listViewItem341,
            listViewItem342,
            listViewItem343,
            listViewItem344,
            listViewItem345,
            listViewItem346,
            listViewItem347,
            listViewItem348,
            listViewItem349,
            listViewItem350,
            listViewItem351,
            listViewItem352,
            listViewItem353,
            listViewItem354,
            listViewItem355,
            listViewItem356,
            listViewItem357,
            listViewItem358,
            listViewItem359,
            listViewItem360,
            listViewItem361,
            listViewItem362,
            listViewItem363,
            listViewItem364,
            listViewItem365,
            listViewItem366,
            listViewItem367,
            listViewItem368,
            listViewItem369,
            listViewItem370,
            listViewItem371,
            listViewItem372,
            listViewItem373,
            listViewItem374,
            listViewItem375,
            listViewItem376,
            listViewItem377,
            listViewItem378,
            listViewItem379,
            listViewItem380,
            listViewItem381,
            listViewItem382,
            listViewItem383,
            listViewItem384,
            listViewItem385,
            listViewItem386,
            listViewItem387,
            listViewItem388,
            listViewItem389,
            listViewItem390,
            listViewItem391,
            listViewItem392,
            listViewItem393,
            listViewItem394,
            listViewItem395,
            listViewItem396,
            listViewItem397,
            listViewItem398,
            listViewItem399,
            listViewItem400,
            listViewItem401,
            listViewItem402,
            listViewItem403,
            listViewItem404,
            listViewItem405,
            listViewItem406,
            listViewItem407,
            listViewItem408,
            listViewItem409,
            listViewItem410,
            listViewItem411,
            listViewItem412,
            listViewItem413,
            listViewItem414,
            listViewItem415,
            listViewItem416,
            listViewItem417,
            listViewItem418,
            listViewItem419,
            listViewItem420,
            listViewItem421,
            listViewItem422,
            listViewItem423,
            listViewItem424,
            listViewItem425,
            listViewItem426,
            listViewItem427,
            listViewItem428,
            listViewItem429});
            // 
            // EquipmentSelectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(725, 584);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "EquipmentSelectionDialog";
            this.ResumeLayout(false);

        }
    }
}
