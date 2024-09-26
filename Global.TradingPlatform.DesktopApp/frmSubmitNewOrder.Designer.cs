namespace Global.TradingPlatform.DesktopApp
{
    partial class frmSubmitNewOrder
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblClordID;
        private TextBox txtClordID;
        private Label lblOrderID;
        private TextBox txtOrderID;
        private Label lblSymbol;
        private TextBox txtSymbol;
        private Label lblSide;
        private TextBox txtSide;
        private Label lblQuantity;
        private TextBox txtQuantity;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lblStatus;
        private TextBox txtStatus;
        private Label lblExecutedQuantity;
        private TextBox txtExecutedQuantity;
        private Label lblRemainingQuantity;
        private TextBox txtRemainingQuantity;
        private Label lblCreatedBy;
        private TextBox txtCreatedBy;
        private Button btnSubmit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSubmitNewOrder));
            lblClordID = new Label();
            txtClordID = new TextBox();
            lblOrderID = new Label();
            txtOrderID = new TextBox();
            lblSymbol = new Label();
            txtSymbol = new TextBox();
            lblSide = new Label();
            txtSide = new TextBox();
            lblQuantity = new Label();
            txtQuantity = new TextBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblStatus = new Label();
            txtStatus = new TextBox();
            lblExecutedQuantity = new Label();
            txtExecutedQuantity = new TextBox();
            lblRemainingQuantity = new Label();
            txtRemainingQuantity = new TextBox();
            lblCreatedBy = new Label();
            txtCreatedBy = new TextBox();
            btnSubmit = new Button();
            btnClear = new Button();
            SuspendLayout();
            // 
            // lblClordID
            // 
            lblClordID.AutoSize = true;
            lblClordID.Location = new Point(10, 10);
            lblClordID.Name = "lblClordID";
            lblClordID.Size = new Size(63, 20);
            lblClordID.TabIndex = 0;
            lblClordID.Text = "ClordID:";
            // 
            // txtClordID
            // 
            txtClordID.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtClordID.Location = new Point(150, 10);
            txtClordID.Name = "txtClordID";
            txtClordID.Size = new Size(238, 27);
            txtClordID.TabIndex = 1;
            // 
            // lblOrderID
            // 
            lblOrderID.AutoSize = true;
            lblOrderID.Location = new Point(10, 40);
            lblOrderID.Name = "lblOrderID";
            lblOrderID.Size = new Size(65, 20);
            lblOrderID.TabIndex = 2;
            lblOrderID.Text = "OrderID:";
            // 
            // txtOrderID
            // 
            txtOrderID.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtOrderID.Location = new Point(150, 40);
            txtOrderID.Name = "txtOrderID";
            txtOrderID.Size = new Size(238, 27);
            txtOrderID.TabIndex = 3;
            // 
            // lblSymbol
            // 
            lblSymbol.AutoSize = true;
            lblSymbol.Location = new Point(10, 70);
            lblSymbol.Name = "lblSymbol";
            lblSymbol.Size = new Size(62, 20);
            lblSymbol.TabIndex = 4;
            lblSymbol.Text = "Symbol:";
            // 
            // txtSymbol
            // 
            txtSymbol.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtSymbol.Location = new Point(150, 70);
            txtSymbol.Name = "txtSymbol";
            txtSymbol.Size = new Size(238, 27);
            txtSymbol.TabIndex = 5;
            txtSymbol.Text = "AAPL";
            // 
            // lblSide
            // 
            lblSide.AutoSize = true;
            lblSide.Location = new Point(10, 100);
            lblSide.Name = "lblSide";
            lblSide.Size = new Size(41, 20);
            lblSide.TabIndex = 6;
            lblSide.Text = "Side:";
            // 
            // txtSide
            // 
            txtSide.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtSide.Location = new Point(150, 100);
            txtSide.Name = "txtSide";
            txtSide.Size = new Size(238, 27);
            txtSide.TabIndex = 7;
            txtSide.Text = "Buy";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(10, 130);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(68, 20);
            lblQuantity.TabIndex = 8;
            lblQuantity.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            txtQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtQuantity.Location = new Point(150, 130);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(238, 27);
            txtQuantity.TabIndex = 9;
            txtQuantity.Text = "1000";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(10, 160);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(44, 20);
            lblPrice.TabIndex = 10;
            lblPrice.Text = "Price:";
            // 
            // txtPrice
            // 
            txtPrice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtPrice.Location = new Point(150, 160);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(238, 27);
            txtPrice.TabIndex = 11;
            txtPrice.Text = "6.5";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(10, 190);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(52, 20);
            lblStatus.TabIndex = 12;
            lblStatus.Text = "Status:";
            // 
            // txtStatus
            // 
            txtStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtStatus.Location = new Point(150, 190);
            txtStatus.Name = "txtStatus";
            txtStatus.Size = new Size(238, 27);
            txtStatus.TabIndex = 13;
            // 
            // lblExecutedQuantity
            // 
            lblExecutedQuantity.AutoSize = true;
            lblExecutedQuantity.Location = new Point(10, 220);
            lblExecutedQuantity.Name = "lblExecutedQuantity";
            lblExecutedQuantity.Size = new Size(132, 20);
            lblExecutedQuantity.TabIndex = 14;
            lblExecutedQuantity.Text = "Executed Quantity:";
            // 
            // txtExecutedQuantity
            // 
            txtExecutedQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtExecutedQuantity.Location = new Point(150, 220);
            txtExecutedQuantity.Name = "txtExecutedQuantity";
            txtExecutedQuantity.Size = new Size(238, 27);
            txtExecutedQuantity.TabIndex = 15;
            // 
            // lblRemainingQuantity
            // 
            lblRemainingQuantity.AutoSize = true;
            lblRemainingQuantity.Location = new Point(10, 250);
            lblRemainingQuantity.Name = "lblRemainingQuantity";
            lblRemainingQuantity.Size = new Size(143, 20);
            lblRemainingQuantity.TabIndex = 16;
            lblRemainingQuantity.Text = "Remaining Quantity:";
            // 
            // txtRemainingQuantity
            // 
            txtRemainingQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtRemainingQuantity.Location = new Point(150, 250);
            txtRemainingQuantity.Name = "txtRemainingQuantity";
            txtRemainingQuantity.Size = new Size(238, 27);
            txtRemainingQuantity.TabIndex = 17;
            // 
            // lblCreatedBy
            // 
            lblCreatedBy.AutoSize = true;
            lblCreatedBy.Location = new Point(10, 280);
            lblCreatedBy.Name = "lblCreatedBy";
            lblCreatedBy.Size = new Size(84, 20);
            lblCreatedBy.TabIndex = 18;
            lblCreatedBy.Text = "Created By:";
            // 
            // txtCreatedBy
            // 
            txtCreatedBy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtCreatedBy.BackColor = SystemColors.HighlightText;
            txtCreatedBy.Location = new Point(150, 280);
            txtCreatedBy.Name = "txtCreatedBy";
            txtCreatedBy.ReadOnly = true;
            txtCreatedBy.Size = new Size(238, 27);
            txtCreatedBy.TabIndex = 19;
            // 
            // btnSubmit
            // 
            btnSubmit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSubmit.Location = new Point(150, 320);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(238, 30);
            btnSubmit.TabIndex = 20;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // btnClear
            // 
            btnClear.BackgroundImageLayout = ImageLayout.Zoom;
            btnClear.Location = new Point(12, 320);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(130, 30);
            btnClear.TabIndex = 20;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // frmSubmitNewOrder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 370);
            Controls.Add(lblClordID);
            Controls.Add(txtClordID);
            Controls.Add(lblOrderID);
            Controls.Add(txtOrderID);
            Controls.Add(lblSymbol);
            Controls.Add(txtSymbol);
            Controls.Add(lblSide);
            Controls.Add(txtSide);
            Controls.Add(lblQuantity);
            Controls.Add(txtQuantity);
            Controls.Add(lblPrice);
            Controls.Add(txtPrice);
            Controls.Add(lblStatus);
            Controls.Add(txtStatus);
            Controls.Add(lblExecutedQuantity);
            Controls.Add(txtExecutedQuantity);
            Controls.Add(lblRemainingQuantity);
            Controls.Add(txtRemainingQuantity);
            Controls.Add(lblCreatedBy);
            Controls.Add(txtCreatedBy);
            Controls.Add(btnClear);
            Controls.Add(btnSubmit);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmSubmitNewOrder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Submit New Order";
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnClear;
    }
}
