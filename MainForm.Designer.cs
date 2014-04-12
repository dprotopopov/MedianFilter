namespace img
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.open = new System.Windows.Forms.ToolStripButton();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.convert = new System.Windows.Forms.ToolStripButton();
            this.median = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.noise = new System.Windows.Forms.ToolStripButton();
            this.left = new System.Windows.Forms.ToolStripButton();
            this.right = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomin = new System.Windows.Forms.ToolStripButton();
            this.zoomout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tool1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tool2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фильтрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mmedian = new System.Windows.Forms.ToolStripMenuItem();
            this.экспериментToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.запускToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mconvert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnoise = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.просмотрСправкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.convert2 = new System.Windows.Forms.ToolStripButton();
            this.mconvert2 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStripContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(905, 388);
            this.panel1.TabIndex = 0;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(905, 363);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(905, 388);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(905, 363);
            this.tabControl1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.open,
            this.save,
            this.toolStripSeparator1,
            this.convert,
            this.convert2,
            this.median,
            this.toolStripSeparator2,
            this.noise,
            this.left,
            this.right,
            this.toolStripSeparator3,
            this.zoomin,
            this.zoomout,
            this.toolStripSeparator4,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(902, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // open
            // 
            this.open.Image = ((System.Drawing.Image)(resources.GetObject("open.Image")));
            this.open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(74, 22);
            this.open.Text = "Открыть";
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // save
            // 
            this.save.Enabled = false;
            this.save.Image = ((System.Drawing.Image)(resources.GetObject("save.Image")));
            this.save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(85, 22);
            this.save.Text = "Сохранить";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // convert
            // 
            this.convert.Enabled = false;
            this.convert.Image = ((System.Drawing.Image)(resources.GetObject("convert.Image")));
            this.convert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.convert.Name = "convert";
            this.convert.Size = new System.Drawing.Size(140, 22);
            this.convert.Text = "Преобразовать в SCI";
            this.convert.Click += new System.EventHandler(this.convert_Click);
            // 
            // median
            // 
            this.median.Enabled = false;
            this.median.Image = ((System.Drawing.Image)(resources.GetObject("median.Image")));
            this.median.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.median.Name = "median";
            this.median.Size = new System.Drawing.Size(159, 22);
            this.median.Text = "Медианная фильтрация";
            this.median.Click += new System.EventHandler(this.median_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // noise
            // 
            this.noise.Enabled = false;
            this.noise.Image = ((System.Drawing.Image)(resources.GetObject("noise.Image")));
            this.noise.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.noise.Name = "noise";
            this.noise.Size = new System.Drawing.Size(108, 22);
            this.noise.Text = "Добавить шум";
            this.noise.Click += new System.EventHandler(this.noise_Click);
            // 
            // left
            // 
            this.left.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.left.Enabled = false;
            this.left.Image = ((System.Drawing.Image)(resources.GetObject("left.Image")));
            this.left.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(23, 22);
            this.left.Text = "Заменить левое правым";
            this.left.Click += new System.EventHandler(this.left_Click);
            // 
            // right
            // 
            this.right.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.right.Enabled = false;
            this.right.Image = ((System.Drawing.Image)(resources.GetObject("right.Image")));
            this.right.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(23, 22);
            this.right.Text = "Заменить правое левым";
            this.right.Click += new System.EventHandler(this.right_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // zoomin
            // 
            this.zoomin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomin.Enabled = false;
            this.zoomin.Image = ((System.Drawing.Image)(resources.GetObject("zoomin.Image")));
            this.zoomin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomin.Name = "zoomin";
            this.zoomin.Size = new System.Drawing.Size(23, 22);
            this.zoomin.Text = "Увеличить изображение";
            this.zoomin.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // zoomout
            // 
            this.zoomout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomout.Enabled = false;
            this.zoomout.Image = ((System.Drawing.Image)(resources.GetObject("zoomout.Image")));
            this.zoomout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomout.Name = "zoomout";
            this.zoomout.Size = new System.Drawing.Size(23, 22);
            this.zoomout.Text = "Уменьшить изображение";
            this.zoomout.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(87, 20);
            this.toolStripButton3.Text = "Настройки";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool1,
            this.tool2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 412);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(905, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tool1
            // 
            this.tool1.Name = "tool1";
            this.tool1.Size = new System.Drawing.Size(0, 17);
            // 
            // tool2
            // 
            this.tool2.Name = "tool2";
            this.tool2.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.фильтрToolStripMenuItem,
            this.экспериментToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(905, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.msave,
            this.toolStripMenuItem1,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // msave
            // 
            this.msave.Enabled = false;
            this.msave.Name = "msave";
            this.msave.Size = new System.Drawing.Size(152, 22);
            this.msave.Text = "Сохранить";
            this.msave.Click += new System.EventHandler(this.msave_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // фильтрToolStripMenuItem
            // 
            this.фильтрToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmedian});
            this.фильтрToolStripMenuItem.Name = "фильтрToolStripMenuItem";
            this.фильтрToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.фильтрToolStripMenuItem.Text = "Фильтр";
            // 
            // mmedian
            // 
            this.mmedian.Enabled = false;
            this.mmedian.Name = "mmedian";
            this.mmedian.Size = new System.Drawing.Size(206, 22);
            this.mmedian.Text = "Медианная фильтрация";
            this.mmedian.Click += new System.EventHandler(this.mmedian_Click);
            // 
            // экспериментToolStripMenuItem
            // 
            this.экспериментToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.запускToolStripMenuItem,
            this.mconvert,
            this.mconvert2,
            this.mnoise});
            this.экспериментToolStripMenuItem.Name = "экспериментToolStripMenuItem";
            this.экспериментToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.экспериментToolStripMenuItem.Text = "Сервис";
            // 
            // запускToolStripMenuItem
            // 
            this.запускToolStripMenuItem.Name = "запускToolStripMenuItem";
            this.запускToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.запускToolStripMenuItem.Text = "Запустить эксперимент";
            this.запускToolStripMenuItem.Click += new System.EventHandler(this.запускToolStripMenuItem_Click);
            // 
            // mconvert
            // 
            this.mconvert.Enabled = false;
            this.mconvert.Name = "mconvert";
            this.mconvert.Size = new System.Drawing.Size(248, 22);
            this.mconvert.Text = "Преобразовать в SCI";
            this.mconvert.Click += new System.EventHandler(this.mconvert_Click);
            // 
            // mnoise
            // 
            this.mnoise.Enabled = false;
            this.mnoise.Name = "mnoise";
            this.mnoise.Size = new System.Drawing.Size(248, 22);
            this.mnoise.Text = "Добавить шум на изображение";
            this.mnoise.Click += new System.EventHandler(this.mnoise_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.toolStripMenuItem3,
            this.просмотрСправкиToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(176, 6);
            // 
            // просмотрСправкиToolStripMenuItem
            // 
            this.просмотрСправкиToolStripMenuItem.Name = "просмотрСправкиToolStripMenuItem";
            this.просмотрСправкиToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.просмотрСправкиToolStripMenuItem.Text = "Просмотр справки";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // convert2
            // 
            this.convert2.Enabled = false;
            this.convert2.Image = ((System.Drawing.Image)(resources.GetObject("convert2.Image")));
            this.convert2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.convert2.Name = "convert2";
            this.convert2.Size = new System.Drawing.Size(148, 22);
            this.convert2.Text = "Преобразовать в BMP";
            this.convert2.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // mconvert2
            // 
            this.mconvert2.Enabled = false;
            this.mconvert2.Name = "mconvert2";
            this.mconvert2.Size = new System.Drawing.Size(248, 22);
            this.mconvert2.Text = "Преобразовать в BMP";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 434);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Фильтрация точечных помех BMP и SCI изображений";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem msave;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фильтрToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mmedian;
        private System.Windows.Forms.ToolStripMenuItem экспериментToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem запускToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton open;
        private System.Windows.Forms.ToolStripButton convert;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripButton median;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mconvert;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem просмотрСправкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnoise;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton noise;
        private System.Windows.Forms.ToolStripButton left;
        private System.Windows.Forms.ToolStripButton right;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton zoomin;
        private System.Windows.Forms.ToolStripButton zoomout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripStatusLabel tool1;
        private System.Windows.Forms.ToolStripStatusLabel tool2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton convert2;
        private System.Windows.Forms.ToolStripMenuItem mconvert2;
    }
}

