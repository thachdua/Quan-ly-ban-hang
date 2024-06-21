﻿using PBL3_QuanLyQuanCafe.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3_QuanLyQuanCafe
{
    public partial class SalaryForm : Form
    {
        BindingSource salaryBinding = new BindingSource();
        //public SalaryForm()
        //{
        //    InitializeComponent();
        //    LoadCbbYear();
        //    cbbMonth.SelectedIndex = 6;
        //    cbbYear.SelectedIndex = 0;
        //    LoadTotalSalary();
        //    AddBinding();
        //}
        //private void LoadCbbYear()
        //{
        //    cbbYear.DataSource = TimeKeepingBLL.Instance.GetListYear();
        //}
        private void AddBinding()
        {
            txtUserName.DataBindings.Add(new Binding("Text", drvTotalSalary.DataSource, "userName"));
            txtDisplayName.DataBindings.Add(new Binding("Text", drvTotalSalary.DataSource, "Tên nhân viên"));
        }
        //private void LoadTotalSalary()
        //{
        //    drvTotalSalary.DataSource = salaryBinding;
        //    string month = cbbMonth.Text;
        //    string year = cbbYear.SelectedValue.ToString();
        //    salaryBinding.DataSource = TimeKeepingBLL.Instance.GetTotalSalary(month, year);
        //    DataTable dt = (DataTable)salaryBinding.DataSource;
        //    float totalSalary = 0;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        totalSalary += float.Parse(dr["Lương"].ToString());
        //    }
        //    lblTotalPrice1.Text = totalSalary.ToString();
        //}

        public SalaryForm()
        {
            InitializeComponent();
            LoadCbbYear();
            if (cbbYear.Items.Count > 0)
            {
                cbbYear.SelectedIndex = 0;  // Đảm bảo một mục được chọn
                cbbMonth.SelectedIndex = 6; // Thiết lập giá trị mặc định cho tháng
                LoadTotalSalary();
                AddBinding();
            }
            else
            {
                MessageBox.Show("Year combo box is empty.");
            }
        }

        private void LoadCbbYear()
        {
            var years = TimeKeepingBLL.Instance.GetListYear();
            if (years == null || years.Count == 0)
            {
                // Xử lý trường hợp không có năm nào được trả về.
                MessageBox.Show("No years available");
                return;
            }
            cbbYear.DataSource = years;
        }

        private void LoadTotalSalary()
        {
            drvTotalSalary.DataSource = salaryBinding;
            string month = cbbMonth.Text;
            if (cbbYear.SelectedValue == null)
            {
                MessageBox.Show("Please select a valid year.");
                return;
            }
            string year = cbbYear.SelectedValue.ToString();
            salaryBinding.DataSource = TimeKeepingBLL.Instance.GetTotalSalary(month, year);
            DataTable dt = (DataTable)salaryBinding.DataSource;
            float totalSalary = 0;
            foreach (DataRow dr in dt.Rows)
            {
                totalSalary += float.Parse(dr["Lương"].ToString());
            }
            lblTotalPrice1.Text = totalSalary.ToString();
        }


        private void cbbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTotalSalary();
        }

        private void cbbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTotalSalary();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                drvPersonalSalary.DataSource = null;
                return;
            }
            else
            {
                string month = cbbMonth.Text;
                string year = cbbYear.SelectedValue.ToString();
                drvPersonalSalary.DataSource = TimeKeepingBLL.Instance.GetTimeKeepingByUserName(txtUserName.Text, month, year);
            }
        }
    }
}
