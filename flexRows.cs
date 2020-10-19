using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace charBuild
{
    //event delegate
    public delegate void Notify(object sender, EventArgs e);
    abstract public class genericRow : Form
    {

        protected Label NameLabel { get; set; }
        protected TextBox NormalDiceBox { get; set; }
        protected TextBox HardDiceBox { get; set; }
        protected TextBox WiggleDiceBox { get; set; }
        protected TextBox CostBox { get; set; }

        protected Boolean IsDeleted = false;

    protected int BaseCost { get; set; }
        public string RowName { get; private set; }

        public genericRow(string _name, Point _location, TabPage _tab, int _baseCost, int _nd=0, int _hd=0, int _wd=0, int _nameWidth=130, int _diceBoxWidth=20, int _costBoxWidth=30)
        {
            //setting object variables
            BaseCost = _baseCost;
            RowName = _name;
            var scrollOffset = _tab.AutoScrollPosition; //makes sure the location of the controls is measured from the top and not the current scroll position
            //creating controls
            NameLabel = new Label();
            NameLabel.Visible = true;
            NameLabel.Name = _name + "-NameLabel";
            NameLabel.Text = _name;
            NameLabel.Location =new Point(_location.X+scrollOffset.X,_location.Y+scrollOffset.Y);

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

            _tab.Controls.Add(NameLabel);
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
        private TextBox CustomName;
        private Button DeleteButton;
        public event Notify customSkillDeleted;
      

        public customSkillRow(string _name, Point _location, TabPage _tab, int _baseCost = 2, int _nd = 2, int _hd = 0, int _wd = 0, int _nameWidth = 130, int _diceBoxWidth = 20, int _costBoxWidth = 30) : base(_name, _location, _tab, _baseCost, _nd, _hd, _wd, _nameWidth, _diceBoxWidth, _costBoxWidth)
        {
            //replacing the name label
            NameLabel.Visible = false;
            CustomName = new TextBox();
            CustomName.Name = _name + "-CustomNameBox";
            CustomName.Text = _name;
            CustomName.Location = new Point(NameLabel.Location.X, NameLabel.Location.Y);
            CustomName.Size = new Size(_nameWidth, 20);
            CustomName.TextAlign = HorizontalAlignment.Center;
            CustomName.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            //creating the delete button
            DeleteButton = new Button();
            DeleteButton.Name = _name + "-Delete";
            DeleteButton.Location=new Point(CostBox.Location.X+_costBoxWidth, CostBox.Location.Y);
            DeleteButton.Size = new Size(30,20);
            DeleteButton.Text = "X";
            DeleteButton.ForeColor = Color.Red;

            DeleteButton.Click += DeleteButton_Click;

            _tab.Controls.Add(CustomName);
            _tab.Controls.Add(DeleteButton);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            customSkillDeleted.Invoke(this, e);
        }

        public void deleteControls(TabPage _tab)
        {
            var toBeMoved = _tab.Controls.OfType<Control>().Where(i=>(i.Location.X>=NameLabel.Location.X)& (i.Location.X <= DeleteButton.Location.X)& (i.Location.Y > NameLabel.Location.Y)); //finds all controlls directly below the deleted one
            foreach(Control ctrl in toBeMoved)
            {
                ctrl.Top -= 20;
            }

            _tab.Controls.Remove(NameLabel);
            _tab.Controls.Remove(CustomName);
            _tab.Controls.Remove(NormalDiceBox);
            _tab.Controls.Remove(HardDiceBox);
            _tab.Controls.Remove(WiggleDiceBox);
            _tab.Controls.Remove(CostBox);
            _tab.Controls.Remove(DeleteButton);

            IsDeleted = true;
        }
    }
}
