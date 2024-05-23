using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;

namespace MushroomPocket
{
    public class MushroomMaster
    {
        public string Name { get; set; }
        public int NoToTransform { get; set; }
        public string TransformTo { get; set; }

        public MushroomMaster(string name, int noToTransform, string transformTo)
        {
            this.Name = name;
            this.NoToTransform = noToTransform;
            this.TransformTo = transformTo;
        }
    }

    public class Character
    {
        [DataType(DataType.Text)]
        public string character_name;
        [DataType(DataType.Text)]
        public int character_hp;
        [DataType(DataType.Text)]
        public int character_exp;
        [DataType(DataType.Text)]
        public string character_skill;

        public class Waluigi : Character
        {
            public Waluigi()
            {
                character_name = "Waluigi";
                Random random = new Random();
                character_hp = random.Next(1, 101);
                character_exp = random.Next(0, 101);
                character_skill = "Agility";
            }
        }

        public class Daisy : Character
        {
            public Daisy()
            {
                character_name = "Daisy";
                Random random = new Random();
                character_hp = random.Next(1, 101);
                character_exp = random.Next(0, 101);
                character_skill = "Leadership";
            }
        }

        public class Wario : Character
        {
            public Wario()
            {
                character_name = "Wario";
                Random random = new Random();
                character_hp = random.Next(1, 101);
                character_exp = random.Next(0, 101);
                character_skill = "Strength";
            }
        }

        public class Luigi : Character
        {
            public Luigi()
            {
                character_name = "Luigi";
                character_hp = 100;
                character_exp = 0;
                character_skill = "Precision and Accuracy";
            }
        }

        public class Peach : Character
        {
            public Peach()
            {
                character_name = "Peach";
                character_hp = 100;
                character_exp = 0;
                character_skill = "Magic Abilities";
            }
        }

        public class Mario : Character
        {
            public Mario()
            {
                character_name = "Mario";
                character_hp = 100;
                character_exp = 0;
                character_skill = "Combat Skills";
            }
        }
    }
}
