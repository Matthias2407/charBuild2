using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace charBuild
{
    abstract public class genericRow : Form
    {

        protected Label NameLabel { get; set; }
        private TextBox NormalDiceBox { get; set; }
        private TextBox HardDiceBox { get; set; }
        private TextBox WiggleDiceBox { get; set; }
        private TextBox CostBox { get; set; }

        private int BaseCost { get; set; }
        public string RowName { get; private set; }

        public genericRow(string _name, Point _location, TabPage _tab, int _baseCost, int _nd=0, int _hd=0, int _wd=0, int _nameWidth=130, int _diceBoxWidth=20, int _costBoxWidth=30)
        {
            //setting object variables
            BaseCost = _baseCost;
            RowName = _name;
            //creating controls
            NameLabel = new Label();
            NameLabel.Name = _name + "-NameLabel";
            NameLabel.Text = _name;
            NameLabel.Location = _location;

            NameLabel.AutoSize = true;
            NameLabel.Anchor = AnchorStyles.Left|AnchorStyles.Top;


            NormalDiceBox = new TextBox();
            NormalDiceBox.Name = _name + "-NormalDiceBox";
            NormalDiceBox.Text = _nd.ToString();
            NormalDiceBox.Location = new Point(NameLabel.Location.X+_nameWidth, NameLabel.Location.Y);
            NormalDiceBox.Size= new Size(_diceBoxWidth, 20);
            NormalDiceBox.TextAlign = HorizontalAlignment.Center;
            NormalDiceBox.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            HardDiceBox = new TextBox();
            HardDiceBox.Name = _name + "-HardDiceBox";
            HardDiceBox.Text = _hd.ToString();
            HardDiceBox.Location = new Point(NormalDiceBox.Location.X + _diceBoxWidth, NormalDiceBox.Location.Y);
            HardDiceBox.Size = new Size(_diceBoxWidth, 20);
            HardDiceBox.TextAlign = HorizontalAlignment.Center;
            HardDiceBox.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            WiggleDiceBox = new TextBox();
            WiggleDiceBox.Name = _name + "-WiggleDiceBox";
            WiggleDiceBox.Text = _wd.ToString();
            WiggleDiceBox.Location = new Point(HardDiceBox.Location.X + _diceBoxWidth, HardDiceBox.Location.Y);
            WiggleDiceBox.Size = new Size(_diceBoxWidth, 20);
            WiggleDiceBox.TextAlign = HorizontalAlignment.Center;
            WiggleDiceBox.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            CostBox = new TextBox();
            CostBox.Name = _name + "-CostBox";
            CostBox.Text = calculateCost(_nd,_hd,_wd).ToString();
            CostBox.Location = new Point(WiggleDiceBox.Location.X + _diceBoxWidth, WiggleDiceBox.Location.Y);
            CostBox.Size = new Size(_costBoxWidth, 20);
            CostBox.TextAlign = HorizontalAlignment.Center;
            CostBox.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            CostBox.ReadOnly = true;

            NormalDiceBox.Leave += DiceBox_Leave;
            HardDiceBox.Leave += DiceBox_Leave;
            WiggleDiceBox.Leave += DiceBox_Leave;

            //adding created controls to the tab

         
            _tab.Controls.Add(NormalDiceBox);
            _tab.Controls.Add(HardDiceBox);
            _tab.Controls.Add(WiggleDiceBox);
            _tab.Controls.Add(CostBox);
        }

        private void DiceBox_Leave(object sender, EventArgs e)
        {
            var send = (TextBox)sender;
            var newValue = 0;
            if(!Int32.TryParse(send.Text, out newValue))
            {
                send.Text = "0";
            }

            CostBox.Text = calculateCost(Int32.Parse(NormalDiceBox.Text), Int32.Parse(HardDiceBox.Text), Int32.Parse(WiggleDiceBox.Text)).ToString();
        }

        private int calculateCost(int _nd=0, int _hd=0, int _wd=0)
        {
            return BaseCost * (_nd + 2*_hd + 4*_wd);
        }


    }

    class abilityRow : genericRow
    {
        public abilityRow(string _name, Point _location, TabPage _tab, int _baseCost=5, int _nd = 2, int _hd = 0, int _wd = 0, int _nameWidth = 130, int _diceBoxWidth = 20, int _costBoxWidth = 30) :base(_name, _location, _tab, _baseCost, _nd, _hd, _wd, _nameWidth, _diceBoxWidth, _costBoxWidth)
        {

        }
    }

    class skillRow : genericRow
    {
        public skillRow(string _name, Point _location, TabPage _tab, int _baseCost = 2, int _nd = 2, int _hd = 0, int _wd = 0, int _nameWidth = 130, int _diceBoxWidth = 20, int _costBoxWidth = 30) : base(_name, _location, _tab, _baseCost, _nd, _hd, _wd, _nameWidth, _diceBoxWidth, _costBoxWidth)
        {

        }
    }

    class customSkillRow : skillRow
    {
        TextBox CustomName;
        public customSkillRow(string _name, Point _location, TabPage _tab, int _baseCost = 2, int _nd = 2, int _hd = 0, int _wd = 0, int _nameWidth = 130, int _diceBoxWidth = 20, int _costBoxWidth = 30) : base(_name, _location, _tab, _baseCost, _nd, _hd, _wd, _nameWidth, _diceBoxWidth, _costBoxWidth)
        {
            NameLabel.Visible = false;
        }
    }
}
