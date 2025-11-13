using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    // √оловна форма програми
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // ѕ≥дписуЇмо обробник кнопки (на випадок, €кщо Designer цього не зробив)
            this.button1.Click += button1_Click;
        }

        // ќбробник натисканн€ кнопки Ч тут демонструЇтьс€ використанн€ ≥ндексатора
        private void button1_Click(object sender, EventArgs e)
        {
            // ---------------------------
            // —творюЇмо "клумбу" на 5 м≥сць.
            // “ут показано типову ситуац≥ю: ми пробуЇмо вставити б≥льше елемент≥в (7),
            // н≥ж дозвол€Ї контейнер (5) Ч ≥ндексатор повинен захистити в≥д помилок.
            // ---------------------------
            FlowerBed bed = new FlowerBed(5);

            // ѕ≥дготуЇмо 7 об'Їкт≥в Flower Ч де€к≥ мають "правильн≥" преф≥кси, де€к≥ Ч н≥.
            // ѕреф≥кси, €к≥ ми вважаЇмо допустимими (див. реал≥зац≥ю FlowerBed):
            // "RO" - Rose (тро€нда), "TU" - Tulip (тюльпан), "LA" - Lavender (лаванда),
            // "OR" - Orchid (орх≥де€), "SU" - Sunflower (сон€шник)
            Flower f1 = new Flower("RO-Red", "Rose", "Red Beauty");
            Flower f2 = new Flower("TU-Yellow", "Tulip", "Yellow Joy");
            Flower f3 = new Flower("LA-Purple", "Lavender", "Purple Calm");
            Flower f4 = new Flower("XX-Unknown", "Mystery", "Strange");   // некоректний преф≥кс -> не додастьс€
            Flower f5 = new Flower("OR-White", "Orchid", "White Grace");
            Flower f6 = new Flower("SU-Tall", "Sunflower", "Giant");
            Flower f7 = new Flower("RO-Pink", "Rose", "Pink Dream");

            // «бер≥гаЇмо у масив дл€ зручност≥ ≥теруванн€
            Flower[] trying = { f1, f2, f3, f4, f5, f6, f7 };

            // –€дки дл€ виводу: л≥ве поле Ч лог спроб, праве Ч усп≥шно додан≥
            string leftLog = "";
            string rightList = "";

            // ѕроходимо по кожн≥й спроб≥ додати кв≥тку у FlowerBed
            for (int i = 0; i < trying.Length; i++)
            {
                // якщо ≥ндекс б≥льше н≥ж дозвол€Ї bed.Length Ч ≥ндексатор в≥дкине вставку.
                // ћи можемо заздалег≥дь перев≥рити або спробувати записати ≥ читати ErrorCode.
                if (i >= bed.Length)
                {
                    // “ут ми симулюЇмо повед≥нку коли ≥ндекс вже поза межами контейнера
                    leftLog += $"{i + 1}.  в≥тку не додано {trying[i].DisplayName} Ч код помилки 1 (поганий ≥ндекс)\n";
                    continue;
                }

                // —проба записати: викликаЇтьс€ сеттер ≥ндексатора
                bed[i] = trying[i];

                // ѕерев≥р€Їмо код помилки п≥сл€ операц≥њ запису
                if (bed.ErrorCode == 0)
                {
                    leftLog += $"{i + 1}.  в≥тку додано {trying[i].DisplayName}\n";

                    // якщо запис пройшов Ч додаЇмо р€док дл€ правого пол€
                    // ћи використовуЇмо об'Їкт, €кий ми т≥льки що додали (trying[i]) Ч це те ж посиланн€,
                    // €ке збер≥гаЇтьс€ у контейнер≥, тому зм≥ст зб≥гаЇтьс€.
                    rightList += $" в≥тка {trying[i].DisplayName} ¬ид- {trying[i].Species} —орт- {trying[i].Variety}\n";
                }
                else if (bed.ErrorCode == 2)
                {
                    leftLog += $"{i + 1}.  в≥тку не додано {trying[i].DisplayName} Ч код помилки 2 (некоректний преф≥кс)\n";
                }
                else // на вс€кий випадок Ч ≥нш≥ коди
                {
                    leftLog += $"{i + 1}.  в≥тку не додано {trying[i].DisplayName} Ч код помилки {bed.ErrorCode}\n";
                }
            }

            // ¬становлюЇмо текст у елементи ≥нтерфейсу
            label1.Text = leftLog.TrimEnd('\n');   // л≥ве поле Ч лог спроб
            label2.Text = rightList.TrimEnd('\n'); // праве поле Ч усп≥шно додан≥ кв≥тки
        }
    }

    // ----------------------------
    //  лас Flower Ч опис одного екземпл€ра кв≥тки
    // ----------------------------
    public class Flower
    {
        // DisplayName Ч загальна назва/≥дентиф≥катор (використовуЇмо перш≥ 2 символи €к "преф≥кс")
        public string DisplayName { get; set; }

        // ¬ид (species) Ч наприклад "Rose", "Tulip" ≥ т.д.
        public string Species { get; set; }

        // ¬ар≥ант/сорт/модель Ч людський текст
        public string Variety { get; set; }

        //  онструктор Ч швидко ≥н≥ц≥ал≥зуЇмо властивост≥
        public Flower(string displayName, string species, string variety)
        {
            DisplayName = displayName;
            Species = species;
            Variety = variety;
        }

        // «ручний ToString (не обов'€зково дл€ лог≥ки)
        public override string ToString()
        {
            return $"{DisplayName} ({Species} Ч {Variety})";
        }
    }

    // ----------------------------
    //  лас FlowerBed Ч контейнер дл€ Flower з ≥ндексатором
    // ----------------------------
    public class FlowerBed
    {
        // ¬нутр≥шн≥й масив дл€ збер≥ганн€ Flower
        private Flower[] flowers;

        // “аблиц€ допустимих преф≥кс≥в (2 л≥тери у верхньому рег≥стр≥)
        // Ќаприклад: "RO" (rose), "TU" (tulip), "LA" (lavender), "OR" (orchid), "SU" (sunflower)
        private string[] validPrefixes;

        //  ≥льк≥сть слот≥в у "клумб≥"
        public int Length { get; private set; }

        // ќстанн≥й код помилки операц≥њ (0 - усп≥х, 1 - поганий ≥ндекс, 2 - поганий преф≥кс)
        public int ErrorCode { get; private set; }

        //  онструктор: створюЇ internal масив ≥ заповнюЇ таблицю преф≥кс≥в
        public FlowerBed(int size)
        {
            flowers = new Flower[size];
            Length = size;
            ErrorCode = 0;
            InitPrefixes();
        }

        // ≤н≥ц≥ал≥зац≥€ допустимих преф≥кс≥в
        private void InitPrefixes()
        {
            validPrefixes = new string[] { "RO", "TU", "LA", "OR", "SU" };
        }

        // ƒопом≥жний метод Ч перев≥рка преф≥ксу
        private bool IsValidPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix)) return false;
            prefix = prefix.ToUpperInvariant();
            foreach (var p in validPrefixes)
            {
                if (p == prefix) return true;
            }
            return false;
        }

        // ƒопом≥жний метод Ч перев≥рка ≥ндексу
        private bool IsValidIndex(int idx)
        {
            return idx >= 0 && idx < Length;
        }

        // ----------------------------
        // ≤ндексатор: дозвол€Ї звертатись до FlowerBed €к до масиву
        // ѕриклад: bed[0] = someFlower; або var f = bed[2];
        // —еттер перев≥р€Ї ≥ндекс ≥ преф≥кс (перш≥ 2 символи DisplayName).
        // ----------------------------
        public Flower this[int index]
        {
            get
            {
                if (!IsValidIndex(index))
                {
                    ErrorCode = 1; // поганий ≥ндекс
                    return null;
                }

                ErrorCode = 0;
                return flowers[index];
            }
            set
            {
                // ѕерев≥р€Їмо ≥ндекс
                if (!IsValidIndex(index))
                {
                    ErrorCode = 1;
                    return;
                }

                // ѕерев≥р€Їмо value на null
                if (value == null || string.IsNullOrEmpty(value.DisplayName) || value.DisplayName.Length < 2)
                {
                    ErrorCode = 2; // вважаЇмо це €к "поганий преф≥кс/≥м'€"
                    return;
                }

                // Ѕеремо перш≥ 2 символи ≥ переводимо у верхн≥й рег≥стр
                string pref = value.DisplayName.Substring(0, 2).ToUpperInvariant();

                // ѕерев≥р€Їмо преф≥кс
                if (!IsValidPrefix(pref))
                {
                    ErrorCode = 2; // поганий преф≥кс
                    return;
                }

                // якщо вс≥ перев≥рки пройшли Ч записуЇмо
                flowers[index] = value;
                ErrorCode = 0;
            }
        }
    }
}
