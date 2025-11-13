using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += button1_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Контейнер на 5 місць
            CaseTransistors MyTr = new CaseTransistors(5, "Bipolar", "AC126", "EbbersMoll");

            // Окремі транзистори
            CaseTransistors MyTr1 = new CaseTransistors(1, "Field-effect", "AC126", "Gummel-Poon");
            CaseTransistors MyTr2 = new CaseTransistors(1, "Field-effect", "AD133", "Gummel-Poon");
            CaseTransistors MyTr3 = new CaseTransistors(1, "Schottky", "BD139", "Gummel-Poon");
            CaseTransistors MyTr4 = new CaseTransistors(1, "Avalanche", "OO117", "EbbersMoll");
            CaseTransistors MyTr5 = new CaseTransistors(1, "Darlington", "BLW34", "EbbersMoll");
            CaseTransistors MyTr6 = new CaseTransistors(1, "Photo", "BU508", "EbbersMoll");
            CaseTransistors MyTr7 = new CaseTransistors(1, "Bipolar", "CLY10", "EbbersMoll");

            // Масив усіх транзисторів
            CaseTransistors[] arr = { MyTr1, MyTr2, MyTr3, MyTr4, MyTr5, MyTr6, MyTr7 };

            string sLeft = "";
            string sRight = "";

            // Додаємо транзистори по одному
            for (int i = 0; i < arr.Length; i++)
            {
                // якщо індекс більший за розмір масиву контейнера — пропускаємо
                if (i >= MyTr.Length)
                {
                    sLeft += $"{i + 1} Транзистор не додано {arr[i].transName} код помилки 1\n";
                    continue;
                }

                MyTr[i] = arr[i];

                if (MyTr.ErrorKod == 0)
                {
                    sLeft += $"{i + 1} Транзистор додано {arr[i].transName}\n";
                    sRight += $"Транзистор {arr[i].transName} Тип- {arr[i].transType} модель- {arr[i].transModelName}\n";
                }
                else
                {
                    sLeft += $"{i + 1} Транзистор не додано {arr[i].transName} код помилки {MyTr.ErrorKod}\n";
                }
            }

            label1.Text = sLeft.TrimEnd('\n');
            label2.Text = sRight.TrimEnd('\n');
        }
    }

    // ---------------- Клас CaseTransistors ----------------
    public class CaseTransistors
    {
        public string transType { get; set; }      // тип транзистора
        public string transName { get; set; }      // назва транзистора
        public string transModelName { get; set; } // модель

        private transPrefixName[] Prefixs;         // таблиця префіксів
        private CaseTransistors[] transistors;     // масив об’єктів

        public int Length;     // кількість елементів
        public int ErrorKod;   // 0 - ок, 1 - індекс, 2 - префікс

        public CaseTransistors(int size, string type, string tname, string modelName)
        {
            transistors = new CaseTransistors[size];
            Length = size;
            transType = type;
            transName = tname;
            transModelName = modelName;
            setPrefixName();
        }

        struct transPrefixName
        {
            public string PrefixName;
            public string PrefixText;
        }

        void setPrefixName()
        {
            Prefixs = new transPrefixName[14];

            Prefixs[0].PrefixName = "AC"; Prefixs[0].PrefixText = "Germanium small-signal AF transistor AC126";
            Prefixs[1].PrefixName = "AD"; Prefixs[1].PrefixText = "Germanium AF power transistor AD133";
            Prefixs[2].PrefixName = "AF"; Prefixs[2].PrefixText = "Germanium small-signal RF transistor AF117";
            Prefixs[3].PrefixName = "AL"; Prefixs[3].PrefixText = "Germanium RF power transistor ALZ10";
            Prefixs[4].PrefixName = "AS"; Prefixs[4].PrefixText = "Germanium switching transistor ASY28";
            Prefixs[5].PrefixName = "AU"; Prefixs[5].PrefixText = "Germanium power switching transistor AU103";
            Prefixs[6].PrefixName = "BC"; Prefixs[6].PrefixText = "Silicon, small signal transistor BC548B";
            Prefixs[7].PrefixName = "BD"; Prefixs[7].PrefixText = "Silicon, power transistor BD139";
            Prefixs[8].PrefixName = "BF"; Prefixs[8].PrefixText = "Silicon, RF transistor BF245";
            Prefixs[9].PrefixName = "BS"; Prefixs[9].PrefixText = "Silicon, switching transistor BS170";
            Prefixs[10].PrefixName = "BL"; Prefixs[10].PrefixText = "Silicon, high power transistor BLW34";
            Prefixs[11].PrefixName = "BU"; Prefixs[11].PrefixText = "Silicon, high voltage transistor BU508";
            Prefixs[12].PrefixName = "CF"; Prefixs[12].PrefixText = "Gallium Arsenide small-signal transistor CF300";
            Prefixs[13].PrefixName = "CL"; Prefixs[13].PrefixText = "Gallium Arsenide power transistor CLY10";
        }

        bool OkPrefixName(string prefix)
        {
            for (int i = 0; i < Prefixs.Length; i++)
            {
                if (Prefixs[i].PrefixName == prefix)
                    return true;
            }
            return false;
        }

        bool OkIndex(int i)
        {
            return i >= 0 && i < Length;
        }

        public CaseTransistors this[int index]
        {
            get
            {
                if (OkIndex(index))
                {
                    ErrorKod = 0;
                    return transistors[index];
                }
                ErrorKod = 1;
                return null;
            }
            set
            {
                if (!OkIndex(index))
                {
                    ErrorKod = 1;
                    return;
                }

                if (value == null || value.transName.Length < 2)
                {
                    ErrorKod = 2;
                    return;
                }

                string prefix = value.transName.Substring(0, 2);
                if (!OkPrefixName(prefix))
                {
                    ErrorKod = 2;
                    return;
                }

                transistors[index] = value;
                ErrorKod = 0;
            }
        }
    }
}
