namespace Lead.Detect.DatabaseHelper
{
    partial class ProductDatabaseControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxProductBarcode = new System.Windows.Forms.TextBox();
            this.buttonQueryProduct = new System.Windows.Forms.Button();
            this.buttonClearAll = new System.Windows.Forms.Button();
            this.buttonQueryAll = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxProductBarcode
            // 
            this.textBoxProductBarcode.Location = new System.Drawing.Point(10, 19);
            this.textBoxProductBarcode.Name = "textBoxProductBarcode";
            this.textBoxProductBarcode.Size = new System.Drawing.Size(214, 21);
            this.textBoxProductBarcode.TabIndex = 17;
            // 
            // buttonQueryProduct
            // 
            this.buttonQueryProduct.Location = new System.Drawing.Point(230, 11);
            this.buttonQueryProduct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonQueryProduct.Name = "buttonQueryProduct";
            this.buttonQueryProduct.Size = new System.Drawing.Size(102, 35);
            this.buttonQueryProduct.TabIndex = 14;
            this.buttonQueryProduct.Text = "查询产品数据";
            this.buttonQueryProduct.UseVisualStyleBackColor = true;
            this.buttonQueryProduct.Click += new System.EventHandler(this.buttonQueryProduct_Click);
            // 
            // buttonClearAll
            // 
            this.buttonClearAll.Location = new System.Drawing.Point(446, 10);
            this.buttonClearAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonClearAll.Name = "buttonClearAll";
            this.buttonClearAll.Size = new System.Drawing.Size(102, 35);
            this.buttonClearAll.TabIndex = 15;
            this.buttonClearAll.Text = "清除所有数据";
            this.buttonClearAll.UseVisualStyleBackColor = true;
            this.buttonClearAll.Click += new System.EventHandler(this.buttonClearAll_Click);
            // 
            // buttonQueryAll
            // 
            this.buttonQueryAll.Location = new System.Drawing.Point(338, 11);
            this.buttonQueryAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonQueryAll.Name = "buttonQueryAll";
            this.buttonQueryAll.Size = new System.Drawing.Size(102, 35);
            this.buttonQueryAll.TabIndex = 16;
            this.buttonQueryAll.Text = "查询全部数据";
            this.buttonQueryAll.UseVisualStyleBackColor = true;
            this.buttonQueryAll.Click += new System.EventHandler(this.buttonQueryAll_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 48);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(579, 343);
            this.dataGridView1.TabIndex = 13;
            // 
            // ProductDatabaseControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.textBoxProductBarcode);
            this.Controls.Add(this.buttonQueryProduct);
            this.Controls.Add(this.buttonClearAll);
            this.Controls.Add(this.buttonQueryAll);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ProductDatabaseControl";
            this.Size = new System.Drawing.Size(600, 400);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxProductBarcode;
        private System.Windows.Forms.Button buttonQueryProduct;
        private System.Windows.Forms.Button buttonClearAll;
        private System.Windows.Forms.Button buttonQueryAll;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
