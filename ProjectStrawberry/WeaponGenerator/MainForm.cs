using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeaponGenerator
{
    public partial class MainForm : Form
    {
        private List<WeaponModel> weapons;
        private List<WeaponType> weaponTypes;
        private int _minDmg = 0;
        private int _maxDmg = 0;
        private int _reqWp = 0;
        private int _weight = 0;
        private int _price = 0;
        private bool _tournamentItem;

        public MainForm()
        {
            weaponTypes = new List<WeaponType>();
            weapons = new List<WeaponModel>();

            InitializeComponent();

            GetWeaponTypes();

        }

        private void GetWeapons(int id)
        {
            var ctx = new ProjectStrawberryEntities();
            weapons = ctx.Weapons.Select(w => new WeaponModel
            {
                Id = w.Id,
                Name = w.Name,
                MinimumDamage = w.MinimumDamage,
                MaximumDamage = w.MaximumDamage,
                ReqWeaponMastery = w.ReqWeaponMastery,
                Weight = w.Weight,
                WeaponTypeId = w.WeaponTypeId,
                WeaponType = w.WeaponType,
                Price = w.Price
            }).Where(w => w.WeaponTypeId == id)
            .OrderBy(w => w.Price).ToList();

            listBoxWeapons.DataSource = weapons;
        }

        private void GetWeaponTypes()
        {
            ProjectStrawberryEntities ctx = new ProjectStrawberryEntities();

            weaponTypes = ctx.WeaponTypes.ToList();

            comboBoxWeaponTypes.DataSource = weaponTypes;
            comboBoxWeaponTypes.DisplayMember = "Name";
            comboBoxWeaponTypes.ValueMember = "Id";
        }

        private void UpdateVariables(object sender, EventArgs e)
        {
            UpdateGui();
        }

        private void UpdateGui()
        {
            _minDmg = Convert.ToInt32(numMinDmg.Value);
            _maxDmg = Convert.ToInt32(numMaxDmg.Value);
            _reqWp = Convert.ToInt32(numReqWp.Value);
            _weight = Convert.ToInt32(numWeight.Value);
            _price = Convert.ToInt32(numPrice.Value);

            double tier = _minDmg + _maxDmg - _weight * 0.5 - _reqWp * 0.10 - _price * 0.02;
            labelTier.Text = "Tier: " + tier.ToString();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (checkBoxTournamentItem.Checked)
                _tournamentItem = true;
            else
                _tournamentItem = false;

            var weapon = new Weapon
            {
                Name = textBoxName.Text,
                WeaponTypeId = (int)comboBoxWeaponTypes.SelectedValue,
                MinimumDamage = _minDmg,
                MaximumDamage = _maxDmg,
                ReqWeaponMastery = _reqWp,
                Weight = _weight,
                TournamentReward = _tournamentItem,
                Price = _price,
            };

            if (!string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                var ctx = new ProjectStrawberryEntities();
                ctx.Weapons.Add(weapon);
                ctx.SaveChanges();
            }

            ResetForm();

            WeaponType wt = (WeaponType)comboBoxWeaponTypes.SelectedItem;
            GetWeapons(wt.Id);
        }

        private void ResetForm()
        {
            _minDmg = 0;
            _maxDmg = 0;
            _reqWp = 0;
            _weight = 0;
            _price = 0;

            numMaxDmg.Value = 0;
            numMinDmg.Value = 0;
            numPrice.Value = 0;
            numReqWp.Value = 0;
            numWeight.Value = 0;

            textBoxName.Text = "";
            labelTier.Text = "Tier: 0";
        }

        private void UpdateListOfWeapons(object sender, EventArgs e)
        {
            WeaponType wt = (WeaponType)comboBoxWeaponTypes.SelectedItem;
            GetWeapons(wt.Id);
        }
    }
}
