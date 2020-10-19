using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace charBuild
{
    public partial class Form1 : Form
    {
        readonly string[] AttributeNames = { "Body", "Coordination", "Sense", "Mind", "Charm", "Command" };
        readonly string[] SkillNames = { "Athletics", "Block", "Brawling", "Endurance",  "Dodge",  "Stealth", "Empathy ", "Scrutiny", "Perception", "First Aid", "Research", "Security Systems", "Streetwise", "Medicine", "Survival", "Navigation", "Tactics", "Lie", "Persuasion", "Interrogation ", "Stability", "Intimidation", "Leadership" };
        readonly string[] CustomSkillNames = { "Melee Weapon (Type)", "Driving (Type)", "Ranged Weapon (Type)", "Knowledge (Type)", "Language (Type)", "Performance (Type)" };

        List<genericRow> allRows=new List<genericRow>();
        int customSkillsNumber = 0;

        public Form1()
        {
            InitializeComponent();

            setupAttributes();

            setupSkills();


            void setupAttributes()
            {
                var offset = 0;
                foreach (string Attr in AttributeNames)
                {
                    allRows.Add(new abilityRow(Attr, new Point(0, offset), tabAbilities));
                    offset += 20;
                }
            }

            void setupSkills()
            {
                var offset = 0;
                foreach (string Skill in SkillNames)
                {
                    allRows.Add(new skillRow(Skill, new Point(0, offset), tabSkills));
                    offset += 20;
                }

                offset = 0;
                foreach (string Skill in CustomSkillNames)
                {
                    var temp = new customSkillRow(Skill, new Point(300, offset), tabSkills);
                    temp.customSkillDeleted += customSkillDeleted;
                    allRows.Add(temp);
                    customSkillsNumber++;
                    offset += 20;
                }

                addSkillButton.Location = new Point(300,offset);
            }
        }

        private void addSkillButton_Click(object sender, EventArgs e)
        {
            var temp = new customSkillRow("CustomSkill" + customSkillsNumber.ToString(), new Point(300, customSkillsNumber * 20), tabSkills);
            temp.customSkillDeleted += customSkillDeleted;
            allRows.Add(temp);

            addSkillButton.Top += 20;
            customSkillsNumber++;
        }

        private void customSkillDeleted(object sender, EventArgs e)
        {
            customSkillRow send = (customSkillRow)sender;

            send.deleteControls(tabSkills);

            customSkillsNumber--;
        }
    }
}
