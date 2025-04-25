using MongoDbProject.Entities;
using MongoDbProject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MongoDbProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ProductOperation operation = new ProductOperation();
        void ProductStatus()
        {
            if (radioButton1.Checked == true)
            {
                txtstatus.Text = "true";
            }
            else if (radioButton2.Checked == true)
            {
                txtstatus.Text = "false";
            }
        }
        void ProductList()
        {
            List<Product> products = operation.GetAllProductList();
            dataGridView1.DataSource = products;
        }
        void GetByIdProductList()
        {
            string productId = txtid.Text;
            Product products = operation.GetProductById(productId);
            dataGridView1.DataSource = new List<Product>() { products };
        }
        void InsertProduct()
        {
            ProductStatus();

            var product = new Product
            {
                ProductName = txtName.Text,
                ProductPrice = Convert.ToInt32(txtPrice.Text),
                ProductStock = Convert.ToInt32(txtStock.Text),
                ProductStatus = Convert.ToBoolean(txtstatus.Text)
            };

            operation.AddProduct(product);
        }
        void UpdateProduct()
        {
            ProductStatus();

            string productId = txtid.Text;

            var product = new Product
            {
                ProductID = productId,
                ProductName = txtName.Text,
                ProductPrice = Convert.ToInt32(txtPrice.Text),
                ProductStock = Convert.ToInt32(txtStock.Text),
                ProductStatus = Convert.ToBoolean(txtstatus.Text)
            };

            operation.UpdateProduct(product);
        }
        void DeleteProduct()
        {
            string productId = txtid.Text;

            operation.DeleteProduct(productId);
        }
        void ClearProduct()
        {
            txtid.Text = "";
            txtName.Text = "";
            txtPrice.Text = "";
            txtStock.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ProductList();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            ProductList();
        }
        private void btnlistbyid_Click(object sender, EventArgs e)
        {
            GetByIdProductList();
            ClearProduct();
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            InsertProduct();
            ProductList();
            ClearProduct();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateProduct();
            ProductList();
            ClearProduct();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteProduct();
            ProductList();
            ClearProduct();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearProduct();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtStock.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
