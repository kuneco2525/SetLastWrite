namespace SetLastWrite;

internal class Main : Form {
	private readonly FolderBrowserDialog DialogDir = new();
	private readonly TextBox TextDir = new();
	private readonly Button ButtonDir = new(), ButtonExecute = new();
	private readonly Label LabelPath = new();

	private void InitializeComponent() {
		SuspendLayout();
		TextDir.ImeMode = ImeMode.Disable;
		TextDir.Location = new Point(12, 12);
		TextDir.Name = "TextOutput";
		TextDir.Size = new Size(200, 23);
		TextDir.TabIndex = 0;
		ButtonDir.FlatStyle = FlatStyle.Popup;
		ButtonDir.Location = new Point(218, 12);
		ButtonDir.Name = "ButtonDir";
		ButtonDir.Size = new Size(23, 23);
		ButtonDir.TabStop = false;
		ButtonDir.Text = "c";
		ButtonDir.Click += new EventHandler(ButtonDir_Click);
		ButtonExecute.FlatStyle = FlatStyle.Popup;
		ButtonExecute.Location = new Point(247, 12);
		ButtonExecute.Name = "ButtonExecute";
		ButtonExecute.Size = new Size(46, 23);
		ButtonExecute.TabIndex = 2;
		ButtonExecute.Text = "ŽÀs";
		ButtonExecute.Click += new EventHandler(ButtonExecute_Click);
		LabelPath.AutoSize = true;
		LabelPath.Location = new Point(12, 38);
		LabelPath.Name = "LabelPath";
		LabelPath.Size = new Size(0, 15);
		BackColor = Color.Honeydew;
		ClientSize = new Size(305, 62);
		Controls.Add(ButtonExecute);
		Controls.Add(LabelPath);
		Controls.Add(TextDir);
		Controls.Add(ButtonDir);
		ForeColor = Color.DarkGreen;
		FormBorderStyle = FormBorderStyle.FixedSingle;
		Name = "Main";
		Text = "SetLastWrite";
		ResumeLayout(false);
		PerformLayout();
	}

	internal Main() => InitializeComponent();

	private static void SetTime(string p) {
		if(File.Exists(p)) {
			FileInfo f = new(p);
			if(f.LastWriteTimeUtc < f.CreationTimeUtc) { File.SetCreationTimeUtc(p, f.LastWriteTimeUtc); }
		} else if(Directory.Exists(p)) {
			foreach(string f in Directory.GetFiles(p)) { SetTime(f); }
			foreach(string d in Directory.GetDirectories(p)) { SetTime(d); }
		}
	}

	private void ButtonDir_Click(object? sender, EventArgs e) { if(DialogDir.ShowDialog(this) == DialogResult.OK) { TextDir.Text = DialogDir.SelectedPath; } }

	private async void ButtonExecute_Click(object? sender, EventArgs e) {
		bool b = false;
		ButtonDir.Enabled = b;
		ButtonExecute.Enabled = b;
		TextDir.Enabled = b;
		await Task.Run(() => SetTime(TextDir.Text));
		Close();
	}
}