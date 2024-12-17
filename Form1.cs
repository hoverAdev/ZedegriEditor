using System.Text.Json;

namespace ZedegriEditor
{
	public partial class Form1 : Form
	{
		public List<ZedegriEnemy> enemyObject = [new()];
		public string lastFile = "";
		public string lastFileS = "";
		public JsonSerializerOptions serializerOptions = new() { WriteIndented = false };

		public void InitControlVals()
		{
			enemyList.BeginUpdate();
			enemyList.Items.Add("XXX");
			enemyList.SelectedIndex = 0;
			enemyList.EndUpdate();
			codeInput.Text = "XXX";
			nameInput.Text = "";
			hpInput.Value = 0;
			defenseInput.Value = 0;
			speedInput.Value = 0;
			zedegriInput.SelectedIndex = 2;

			chanceInput1.Value = 0;
			damageInput1.Value = 0;
			accuracyInput1.Value = 0;
			healInput1.SelectedIndex = 0;
			healPercInput1.Value = 0;
			staggerInput1.SelectedIndex = 2;
			leechInput1.Checked = false;
			atkBuffTypeInput1.SelectedIndex = 0;
			defBuffTypeInput1.SelectedIndex = 0;
			atkBuffTurnInput1.Value = 0;
			defBuffTurnInput1.Value = 0;

			chanceInput2.Value = 0;
			damageInput2.Value = 0;
			accuracyInput2.Value = 0;
			healInput2.SelectedIndex = 0;
			healPercInput2.Value = 0;
			staggerInput2.SelectedIndex = 2;
			leechInput2.Checked = false;
			atkBuffTypeInput2.SelectedIndex = 0;
			defBuffTypeInput2.SelectedIndex = 0;
			atkBuffTurnInput2.Value = 0;
			defBuffTurnInput2.Value = 0;

			chanceInput3.Value = 0;
			damageInput3.Value = 0;
			accuracyInput3.Value = 0;
			healInput3.SelectedIndex = 0;
			healPercInput3.Value = 0;
			staggerInput3.SelectedIndex = 2;
			leechInput3.Checked = false;
			atkBuffTypeInput3.SelectedIndex = 0;
			defBuffTypeInput3.SelectedIndex = 0;
			atkBuffTurnInput3.Value = 0;
			defBuffTurnInput3.Value = 0;

			enemyList.Focus();
		}

		public Form1()
		{
			InitializeComponent();
			InitControlVals();
		}

		private void enemyList_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			if (index != -1)
			{
				codeInput.Text = enemyObject[index].code;
				nameInput.Text = enemyObject[index]._name;
				hpInput.Value = enemyObject[index].hp;
				defenseInput.Value = enemyObject[index].def;
				speedInput.Value = enemyObject[index]._speed;
				zedegriInput.SelectedIndex = enemyObject[index].IsZedegri;

				chanceInput1.Value = enemyObject[index].Attack_1.chance;
				damageInput1.Value = enemyObject[index].Attack_1.damage;
				accuracyInput1.Value = enemyObject[index].Attack_1.accuracy;
				healInput1.SelectedIndex = enemyObject[index].Attack_1.IsHeal;
				healPercInput1.Value = enemyObject[index].Attack_1.HealPercent;
				staggerInput1.SelectedIndex = enemyObject[index].Attack_1.Stagger;
				leechInput1.Checked = enemyObject[index].Attack_1.IsLeech;
                atkBuffTypeInput1.SelectedIndex = enemyObject[index].Attack_1.AttackBuff[0];
				defBuffTypeInput1.SelectedIndex = enemyObject[index].Attack_1.DefenceBuff[0];
				atkBuffTurnInput1.Value = enemyObject[index].Attack_1.AttackBuff[1];
				defBuffTurnInput1.Value = enemyObject[index].Attack_1.DefenceBuff[1];

				chanceInput2.Value = enemyObject[index].Attack_2.chance;
				damageInput2.Value = enemyObject[index].Attack_2.damage;
				accuracyInput2.Value = enemyObject[index].Attack_2.accuracy;
				healInput2.SelectedIndex = enemyObject[index].Attack_2.IsHeal;
				healPercInput2.Value = enemyObject[index].Attack_2.HealPercent;
				staggerInput2.SelectedIndex = enemyObject[index].Attack_2.Stagger;
                leechInput2.Checked = enemyObject[index].Attack_2.IsLeech;
                atkBuffTypeInput2.SelectedIndex = enemyObject[index].Attack_2.AttackBuff[0];
				defBuffTypeInput2.SelectedIndex = enemyObject[index].Attack_2.DefenceBuff[0];
				atkBuffTurnInput2.Value = enemyObject[index].Attack_2.AttackBuff[1];
				defBuffTurnInput2.Value = enemyObject[index].Attack_2.DefenceBuff[1];

				chanceInput3.Value = enemyObject[index].Attack_3.chance;
				damageInput3.Value = enemyObject[index].Attack_3.damage;
				accuracyInput3.Value = enemyObject[index].Attack_3.accuracy;
				healInput3.SelectedIndex = enemyObject[index].Attack_3.IsHeal;
				healPercInput3.Value = enemyObject[index].Attack_3.HealPercent;
				staggerInput3.SelectedIndex = enemyObject[index].Attack_3.Stagger;
                leechInput3.Checked = enemyObject[index].Attack_3.IsLeech;
                atkBuffTypeInput3.SelectedIndex = enemyObject[index].Attack_3.AttackBuff[0];
				defBuffTypeInput3.SelectedIndex = enemyObject[index].Attack_3.DefenceBuff[0];
				atkBuffTurnInput3.Value = enemyObject[index].Attack_3.AttackBuff[1];
				defBuffTurnInput3.Value = enemyObject[index].Attack_3.DefenceBuff[1];
			}
		}

		private void enemyAddButton_Click(object sender, EventArgs e)
		{
			enemyObject.Add(new());
			enemyList.Items.Add("XXX");
		}
		private void enemyDelButton_Click(object sender, EventArgs e)
		{
			if (enemyList.Items.Count > 1)
			{
				enemyObject.RemoveAt(enemyList.SelectedIndex);
				enemyList.Items.RemoveAt(enemyList.SelectedIndex);
			}
		}

		private void loadButton_Click(object sender, EventArgs e)
		{
			openFileDialog.ShowDialog();
		}
		private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string raw = File.ReadAllText(openFileDialog.FileName);
			try
			{
				List<ZedegriEnemy> deserialize = JsonSerializer.Deserialize<List<ZedegriEnemy>>(raw);
				// Check if file is valid
				if (deserialize == null)
				{
					return;
				}

				// Set reloadFile to work properly
				lastFile = openFileDialog.FileName;
				reloadFile.Text = $"Reload {openFileDialog.FileName.Split("\\")[^1]}";

				setupForm(deserialize);
			}
			catch (System.Text.Json.JsonException)
			{
				return;
			}
		}
		private void reloadFile_Click(object sender, EventArgs e)
		{
			if (lastFile != "")
			{
				string raw = File.ReadAllText(lastFile);
				try
				{
					List<ZedegriEnemy> deserialize = JsonSerializer.Deserialize<List<ZedegriEnemy>>(raw);
					// Check if file is valid
					if (deserialize == null)
					{
						return;
					}

					setupForm(deserialize);
				}
				catch (System.Text.Json.JsonException)
				{
					return;
				}
			}
			else { openFileDialog.ShowDialog(); }
		}
		public void setupForm(List<ZedegriEnemy> deserialize)
		{
			enemyObject.Clear();
			enemyList.BeginUpdate();
			enemyList.Items.Clear();
			foreach (ZedegriEnemy enemy in deserialize)
			{
				enemyObject.Add(enemy);
				enemyList.Items.Add(enemy.code);
			}
			enemyList.SelectedIndex = 0;
			enemyList.EndUpdate();

			ZedegriEnemy firstEnemy = deserialize[0];

			codeInput.Text = firstEnemy.code;
			nameInput.Text = firstEnemy._name;
			hpInput.Value = firstEnemy.hp;
			defenseInput.Value = firstEnemy.def;
			speedInput.Value = firstEnemy._speed;
			zedegriInput.SelectedIndex = firstEnemy.IsZedegri;

			chanceInput1.Value = firstEnemy.Attack_1.chance;
			damageInput1.Value = firstEnemy.Attack_1.damage;
			accuracyInput1.Value = firstEnemy.Attack_1.accuracy;
			healInput1.SelectedIndex = firstEnemy.Attack_1.IsHeal;
			healPercInput1.Value = firstEnemy.Attack_1.HealPercent;
			staggerInput1.SelectedIndex = firstEnemy.Attack_1.Stagger;
			leechInput1.Checked = firstEnemy.Attack_1.IsLeech;
			atkBuffTypeInput1.SelectedIndex = firstEnemy.Attack_1.AttackBuff[0];
			defBuffTypeInput1.SelectedIndex = firstEnemy.Attack_1.DefenceBuff[0];
			atkBuffTurnInput1.Value = firstEnemy.Attack_1.AttackBuff[1];
			defBuffTurnInput1.Value = firstEnemy.Attack_1.DefenceBuff[1];

			chanceInput2.Value = firstEnemy.Attack_2.chance;
			damageInput2.Value = firstEnemy.Attack_2.damage;
			accuracyInput2.Value = firstEnemy.Attack_2.accuracy;
			healInput2.SelectedIndex = firstEnemy.Attack_2.IsHeal;
			healPercInput2.Value = firstEnemy.Attack_2.HealPercent;
			staggerInput2.SelectedIndex = firstEnemy.Attack_2.Stagger;
            leechInput2.Checked = firstEnemy.Attack_2.IsLeech;
            atkBuffTypeInput2.SelectedIndex = firstEnemy.Attack_2.AttackBuff[0];
			defBuffTypeInput2.SelectedIndex = firstEnemy.Attack_2.DefenceBuff[0];
			atkBuffTurnInput2.Value = firstEnemy.Attack_2.AttackBuff[1];
			defBuffTurnInput2.Value = firstEnemy.Attack_2.DefenceBuff[1];

			chanceInput3.Value = firstEnemy.Attack_3.chance;
			damageInput3.Value = firstEnemy.Attack_3.damage;
			accuracyInput3.Value = firstEnemy.Attack_3.accuracy;
			healInput3.SelectedIndex = firstEnemy.Attack_3.IsHeal;
			healPercInput3.Value = firstEnemy.Attack_3.HealPercent;
			staggerInput3.SelectedIndex = firstEnemy.Attack_3.Stagger;
            leechInput3.Checked = firstEnemy.Attack_3.IsLeech;
            atkBuffTypeInput3.SelectedIndex = firstEnemy.Attack_3.AttackBuff[0];
			defBuffTypeInput3.SelectedIndex = firstEnemy.Attack_3.DefenceBuff[0];
			atkBuffTurnInput3.Value = firstEnemy.Attack_3.AttackBuff[1];
			defBuffTurnInput3.Value = firstEnemy.Attack_3.DefenceBuff[1];
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			saveFileDialog.ShowDialog();
		}
		private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			resaveButton.Text = saveFileDialog.FileName.Split("\\")[^1];
			lastFileS = saveFileDialog.FileName;

			var serialize = JsonSerializer.Serialize(enemyObject, serializerOptions);
			serialize = serialize.Insert(1, "\n");
			serialize = serialize.Replace("]}},", "]}},\n");
			serialize = serialize.Insert(serialize.Length - 1, "\n");
			File.WriteAllText(saveFileDialog.FileName, serialize);
		}
		private void resaveButton_Click(object sender, EventArgs e)
		{
			if (lastFileS != "")
			{
				var serialize = JsonSerializer.Serialize(enemyObject, serializerOptions);
				File.WriteAllText(lastFileS, serialize);
			}
			else { saveFileDialog.ShowDialog(); }
		}

		private void codeInput_TextChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].code = codeInput.Text;
			enemyList.Items[index] = codeInput.Text;
			enemyList.SelectedIndex = index;
		}
		private void nameInput_TextChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index]._name = nameInput.Text;
		}
		private void hpInput_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].hp = (ulong)hpInput.Value;
		}
		private void defenseInput_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].def = (ulong)defenseInput.Value;
		}
		private void speedInput_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index]._speed = (byte)speedInput.Value;
		}
		private void zedegriInput_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].IsZedegri = (byte)zedegriInput.SelectedIndex;
		}

		private void chanceInput1_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].Attack_1.chance = (byte)chanceInput1.Value;
		}
		private void chanceInput2_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].Attack_2.chance = (byte)chanceInput2.Value;
		}
		private void chanceInput3_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].Attack_3.chance = (byte)chanceInput3.Value;
		}

		private void damageInput1_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].Attack_1.damage = (ulong)damageInput1.Value;
		}
		private void damageInput2_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].Attack_2.damage = (ulong)damageInput2.Value;
		}
		private void damageInput3_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].Attack_3.damage = (ulong)damageInput3.Value;
		}

		private void accuracyInput1_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].Attack_1.accuracy = (byte)accuracyInput1.Value;
		}
		private void accuracyInput2_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].Attack_2.accuracy = (byte)accuracyInput2.Value;
		}
		private void accuracyInput3_ValueChanged(object sender, EventArgs e)
		{
			int index = enemyList.SelectedIndex;
			enemyObject[index].Attack_3.accuracy = (byte)accuracyInput3.Value;
		}

		private void healInput1_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_1.IsHeal = (byte)healInput1.SelectedIndex;
		}
		private void healInput2_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_2.IsHeal = (byte)healInput2.SelectedIndex;
		}
		private void healInput3_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_3.IsHeal = (byte)healInput3.SelectedIndex;
		}

		private void healPercInput1_ValueChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_1.HealPercent = (byte)healPercInput1.Value;
		}
		private void healPercInput2_ValueChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_2.HealPercent = (byte)healPercInput2.Value;
		}
		private void healPercInput3_ValueChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_3.HealPercent = (byte)healPercInput3.Value;
		}

		private void staggerInput1_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_1.Stagger = (byte)staggerInput1.SelectedIndex;
		}
		private void staggerInput2_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_2.Stagger = (byte)staggerInput2.SelectedIndex;
		}
		private void staggerInput3_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_3.Stagger = (byte)staggerInput3.SelectedIndex;
		}

		private void leechInput1_CheckedChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_1.IsLeech = leechInput1.Checked;
		}
		private void leechInput2_CheckedChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_2.IsLeech = leechInput2.Checked;
		}
		private void leechInput3_CheckedChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_3.IsLeech = leechInput3.Checked;
		}

		private void atkBuffTypeInput1_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_1.AttackBuff[0] = (byte)atkBuffTypeInput1.SelectedIndex;
		}
		private void atkBuffTypeInput2_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_2.AttackBuff[0] = (byte)atkBuffTypeInput2.SelectedIndex;
		}
		private void atkBuffTypeInput3_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_3.AttackBuff[0] = (byte)atkBuffTypeInput3.SelectedIndex;
		}

		private void defBuffTypeInput1_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_1.DefenceBuff[0] = (byte)defBuffTypeInput1.SelectedIndex;
		}
		private void defBuffTypeInput2_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_2.DefenceBuff[0] = (byte)defBuffTypeInput2.SelectedIndex;
		}
		private void defBuffTypeInput3_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_3.DefenceBuff[0] = (byte)defBuffTypeInput3.SelectedIndex;
		}

		private void atkBuffTurnInput1_ValueChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_1.AttackBuff[1] = (byte)atkBuffTurnInput1.Value;
		}
		private void atkBuffTurnInput2_ValueChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_2.AttackBuff[1] = (byte)atkBuffTurnInput2.Value;
		}
		private void atkBuffTurnInput3_ValueChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_3.AttackBuff[1] = (byte)atkBuffTurnInput3.Value;
		}

		private void defBuffTurnInput1_ValueChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_1.DefenceBuff[1] = (byte)defBuffTurnInput1.Value;
		}
		private void defBuffTurnInput2_ValueChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_2.DefenceBuff[1] = (byte)defBuffTurnInput2.Value;
		}
		private void defBuffTurnInput3_ValueChanged(object sender, EventArgs e)
		{
			var index = enemyList.SelectedIndex;
			enemyObject[index].Attack_3.DefenceBuff[1] = (byte)defBuffTurnInput3.Value;
		}
	}

	public class ZedegriEnemy
	{
		public string code { get; set; }
		public string _name { get; set; }
		public ulong hp { get; set; }
		public ulong def { get; set; }
		public byte _speed { get; set; }
		public byte IsZedegri { get; set; }
		public ZedAttack Attack_1 { get; set; }
		public ZedAttack Attack_2 { get; set; }
		public ZedAttack Attack_3 { get; set; }

		public ZedegriEnemy() {
			code = "XXX";
			_name = "";
			hp = 0;
			def = 0;
			_speed = 0;
			IsZedegri = 2;
			Attack_1 = new();
			Attack_2 = new();
			Attack_3 = new();
		}
	}
	public class ZedAttack
	{
		public byte chance { get; set; }
		public ulong damage { get; set; }
		public byte accuracy { get; set; }
		public byte IsHeal { get; set; }
		public byte HealPercent { get; set; }
		public byte Stagger { get; set; }
		public bool IsLeech {  get; set; }
		public List<byte> AttackBuff{ get; set; }
		public List<byte> DefenceBuff { get; set; }

		public ZedAttack()
		{
			chance = 0;
			damage = 0;
			accuracy = 0;
			IsHeal = 0;
			HealPercent = 0;
			Stagger = 2;
			IsLeech = false;
			AttackBuff = [0, 0];
			DefenceBuff = [0, 0];
		}
	}
}
