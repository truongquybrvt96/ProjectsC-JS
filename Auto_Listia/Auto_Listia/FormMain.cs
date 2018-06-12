using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Web;
using HtmlAgilityPack;
using System.Diagnostics;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;

namespace Auto_Listia
{

    public partial class FormMain : Form
    {

        private static string appGuid = Application.ProductName.ToString();
        private static Mutex mutex;
        WebClient wc = new WebClient();
        string mau_mota = "";

        string workbookPath = Application.StartupPath + @"\data\danhsach.xlsx";
        Excel excel;



        static ReadOnlyCollection<TimeZoneInfo> tzCollection = TimeZoneInfo.GetSystemTimeZones();
        TimeZoneInfo hanoi = tzCollection.FirstOrDefault(n => n.DisplayName.ToLower().Contains("hanoi"));

        IWebDriver driver, driver_mota;
        string tk, pass;
        public FormMain()
        {
            InitializeComponent();
            bool mutexCreated;
            mutex = new Mutex(true, "Global\\" + appGuid, out mutexCreated);
            if (mutexCreated)
                mutex.ReleaseMutex();

            if (!mutexCreated)
            {
                //App is already running, close this!
                MessageBox.Show("Ứng dụng " + Application.ProductName.ToString() +" đang chạy");
                Environment.Exit(0); //i used this because its a console app
            }

            System.Net.ServicePointManager.Expect100Continue = false;
            excel = new Excel(workbookPath, 1);
            using (StreamReader sw = new StreamReader("data/taikhoan.txt"))
            {
                try
                {
                    if (sw != null)
                    {
                        tk = sw.ReadLine().Trim();
                        pass = sw.ReadLine().Trim();
                    }
                }
                catch
                {
                    MessageBox.Show("Không thể đọc file taikhoan.txt");
                }
            }

            using (StreamReader sw = new StreamReader("data/mota.txt"))
            {
                try
                {
                    if (sw != null)
                    {
                        mau_mota = sw.ReadToEnd();
                    }
                }
                catch
                {
                    MessageBox.Show("Không thể đọc file mota.txt");
                }
            }
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            driver = new ChromeDriver(driverService, chromeOptions);

        }

        void Laythongtinsanpham(string url)
        {

            Thread m_thread = new Thread(t =>
            {
                try
                {
                    int soanhcanlay = 0;
                    this.Invoke((MethodInvoker)delegate
                        {
                            if (txtSoAnhCanLay.Text != "")
                                soanhcanlay = int.Parse(txtSoAnhCanLay.Text);
                        });
                    int img_number = 0;

                    string price = "";
                    string shipping = "";


                    HtmlWeb htmlWeb = new HtmlWeb()
                    {
                        AutoDetectEncoding = false,
                        OverrideEncoding = Encoding.UTF8
                    };
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    document = htmlWeb.Load(url);


                    price = document.DocumentNode.SelectSingleNode("//span[@class='notranslate']").InnerText; //document.DocumentNode.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("notranslate")).ElementAt(0).InnerText;
                    shipping = document.DocumentNode.SelectSingleNode("//*[@id='fshippingCost']/span").InnerText;


                    this.Invoke((MethodInvoker)delegate
                        {
                            txtGiaGoc_raw.Text = price;
                            txtgiaShip_raw.Text = shipping;
                        });
                    price = price.Replace(" ", string.Empty).Replace("$", string.Empty);
                    shipping = shipping.Replace("$", string.Empty).Replace(" ", string.Empty);

                    if (shipping.ToLower().Equals("free"))
                    {
                        this.Invoke((MethodInvoker)delegate
                            {
                                txtGiaShip_USD.Text = "0";
                            });
                    }
                    else
                    {
                        Convert_Curency(shipping, "ship");
                    }
                    if (!price.Equals(""))
                    {
                        Convert_Curency(price, "price");
                    }

                    //TIEU DE

                    //document.DocumentNode.Descendants("h1").Where(node => node.GetAttributeValue("id", "").Equals("itemTitle")).ElementAt(0).InnerText
                    string tieude = HttpUtility.HtmlDecode(document.DocumentNode.SelectSingleNode("//h1[@id='itemTitle']").InnerText);


                    this.Invoke((MethodInvoker)delegate
                        {
                            txtTieuDe.Text = tieude.Replace("Details about ", string.Empty).Trim();
                            lblDoDaiTieuDe.Text = txtTieuDe.TextLength.ToString() + "/100";
                        });
                    int d = 1;
                    Thread thread2 = new Thread(t1 =>
                        {
                            try
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    lblTrangThaiAnh.Text = "...";
                                    lblTrangThaiAnh.BackColor = Color.Yellow;
                                });
                                int i = 1;

                                List<string> arr_img = new List<string>();
                                clearFolder("anh");

                                foreach (HtmlNode img in document.DocumentNode.SelectNodes("//div[@id='PicturePanel']//div//img"))
                                {
                                    string s = img.GetAttributeValue("src", "").ToString();
                                    if (!s.Contains(".gif"))
                                    {
                                        if (img_number > soanhcanlay + 1)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (s.Contains("ebayimg.com"))
                                            {
                                                //images/g/
                                                string s1 = s.Substring(s.IndexOf("images/g/") + 9);
                                                //MessageBox.Show("s1: " + s1);
                                                string s2 = s1.Substring(0, s1.Length - (s1.Length - s1.IndexOf("/s-")));

                                                if (!check_if_item_exist_in_list(arr_img, s2))
                                                {
                                                    arr_img.Add(s2);
                                                    string url_rep = "https://i.ebayimg.com/images/g/##/s-l1600.jpg";
                                                    Uri img_uri = new Uri(url_rep.Replace("##", s2));
                                                    wc.DownloadFile(img_uri, "anh/" + s2 + ".jpg");

                                                    this.Invoke((MethodInvoker)delegate
                                                    {
                                                        lblTrangThaiAnh.Text = d.ToString();
                                                        d++;
                                                    });


                                                }
                                            }
                                            else
                                            {
                                                i++;
                                                Uri img_uri1 = new Uri(s);
                                                wc.DownloadFile(img_uri1, "anh/" + i + ".jpg");
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    lblTrangThaiAnh.Text = d.ToString();
                                                    d++;
                                                });

                                            }
                                            img_number++;
                                        }

                                    }
                                }
                                this.Invoke((MethodInvoker)delegate
                                {

                                    lblTrangThaiAnh.BackColor = Color.LightGreen;
                                });
                            }
                            catch
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    lblTrangThaiAnh.BackColor = Color.Red;
                                    d = 1;
                                });
                            }
                        })
                    { IsBackground = true };
                    thread2.Start();
                    this.Invoke((MethodInvoker)delegate
                        {
                            float giasp = StringTofloat(txtGiaGoc_USD.Text);
                            float giaship = StringTofloat(txtGiaShip_USD.Text);
                            float giatong = giasp + giaship;
                            txtTong.Text = FloatToString(giatong);

                            float tiencong = StringTofloat(txtTienCong.Text);
                            float phipaypal = StringTofloat(txtPhiPayPal.Text) / 100;
                            float giaTuTinh = (float)Math.Round((double)(giasp + giaship + tiencong) + (giasp + giaship + tiencong) * phipaypal, 2);
                            txtGiaBan_TuTinh.Text = FloatToString(giaTuTinh);
                            lblGiaban_Tong_Nhan.Text = txtTong.Text;
                            TinhGia_Nhan();
                        });

                    this.Invoke((MethodInvoker)delegate
                        {
                            lblTrangThai.Text = "Xong";
                            lblTrangThai.BackColor = Color.LightGreen;
                        });

                }
                catch

                {

                    this.Invoke((MethodInvoker)delegate
                    {
                        lblTrangThai.Text = "Lỗi";
                        lblTrangThai.BackColor = Color.Red;
                    });

                }
            })
            { IsBackground = true };
            m_thread.Start();


        }

        private void clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                clearFolder(di.FullName);
                di.Delete();
            }
        }
        bool check_if_item_exist_in_list(List<string> a, string c)
        {
            if (a.Count == 0)
                return false;
            bool key = false;
            foreach (string s in a)
            {
                if (s == c)
                    key = true;
            }
            return key ? true : false;
        }

        string extract_number(string money_input, ref string type)
        {
            if (money_input.Contains(","))
                money_input = money_input.Replace(",", string.Empty);
            if (money_input.Contains(" "))
                money_input = money_input.Replace(" ", string.Empty);
            string result = "";
            string numberStr = string.Empty;
            for (int i = 0; i < money_input.Length; i++)
            {
                char c = money_input[i];
                // if code of char is between code of '0' and '9'
                if (c >= '0' && c <= '9' || c == '.')
                {
                    numberStr += c;
                    // if is the last char of string, add the last number
                    if (i == money_input.Length - 1)
                    {
                        result = numberStr;
                    }
                }
                // if char is not a number and numberStr is not empty
                else if (!string.IsNullOrWhiteSpace(numberStr))
                {
                    result = numberStr; ; // add the new number
                    numberStr = string.Empty; // clean
                }
            }
            type = money_input.Replace(result, string.Empty);
            return result;
        }
        void HamLayThongTin()
        {
            if (txtUrlSanPham.Text!="")
            {
                string url = txtUrlSanPham.Text;
                Laythongtinsanpham(url);
                enable_button(true);
            }
            

            //float giasp = StringTofloat(txtGiaGoc_USD.Text);
            //float giaship = StringTofloat(txtGiaShip_USD.Text);
            //float giatong = giasp + giaship;
            //txtTong.Text = FloatToString(giatong);

            //float tiencong = StringTofloat(txtTienCong.Text);
            //float phipaypal = StringTofloat(txtPhiPayPal.Text) / 100;
            //float giaTuTinh = (float)Math.Round((double)(giasp + giaship + tiencong) + (giasp + giaship + tiencong) * phipaypal, 2);
            //txtGiaBan_TuTinh.Text = FloatToString(giaTuTinh);
            //lblGiaban_Tong_Nhan.Text = txtTong.Text;
            //TinhGia_Nhan();
            
        }

        private float StringTofloat(string stringVal)
        {
            if (stringVal.Contains("."))
                stringVal = stringVal.Replace(".", ",");
            return System.Convert.ToSingle(stringVal);
        }

        private string FloatToString(float fVal)
        {
            return fVal.ToString().Replace(",", ".");
        }

        string url_curency = string.Empty;
        void Convert_Curency(string raw_money, string n)
        {
            //Thread m_thread = new Thread(t =>
            //{

            string real_money = "";
            string type = "";
            string price = extract_number(raw_money, ref type);
            string raw_url = "https://finance.google.com/finance/converter";
            string amount = "";
            string from = "";
            if (type != "US" && type != "USD" && type != "")
            {
                if (type == "C")
                    type = "CAD";
                if (type == "AU")
                    type = "AUD";
                if (type == "Fr")
                    type = "CHF";
                if (type == "€")
                    type = "EUR";
                amount = "?a=" + price.ToString();
                from = "&from=" + type;
                url_curency = raw_url + amount + from + "&to=USD";
                real_money = Transfer(url_curency);
                if (n == "ship")
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtGiaShip_USD.Text = real_money;
                    });

                }
                if (n == "price")
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtGiaGoc_USD.Text = real_money;
                    });

                }
            }
            else
            {
                if (n == "price")
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtGiaGoc_USD.Text = price;
                    });

                }
                if (n == "ship")
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtGiaShip_USD.Text = price;
                    });

                }
            }
            //})
            //{ IsBackground = true };
            //m_thread.Start();
        }

        string Transfer(string url)
        {
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            };
            HtmlAgilityPack.HtmlDocument document = htmlWeb.Load(url);
            return document.DocumentNode.SelectSingleNode("//span[@class='bld']").InnerText.Replace(" ", string.Empty).Replace("USD", string.Empty);
        }
        private void btnLayMoTa_Click(object sender, EventArgs e)
        {

            string url = txtUrlSanPham.Text;
            Thread thread2 = new Thread(t =>
            {
                //rtxtMota.Text = url;
                string item_number = url.Substring(url.LastIndexOf('/') + 1);
                string mota_url = "http://vi.vipr.ebaydesc.com/ws/eBayISAPI.dll?ViewItemDescV4&item=" + item_number;
                //Process.Start(GetDefaultBrowserPath(), "http://vi.vipr.ebaydesc.com/ws/eBayISAPI.dll?ViewItemDescV4&item=" + item_number);

                var driverService1 = ChromeDriverService.CreateDefaultService();
                driverService1.HideCommandPromptWindow = true;
                driver_mota = new ChromeDriver(driverService1, new ChromeOptions());

                driver_mota.Navigate().GoToUrl(mota_url);
                driver_mota.Manage().Window.Size = new System.Drawing.Size(700, 700);
            })
            { IsBackground = true };
            thread2.Start();

        }

        void enable_button(bool b)
        {
            if (!b)
            {
                //btnLayMoTa.Enabled = false;
                //btnList.Enabled = false;
                lblTrangThai.Text = "Trống";
                lblTrangThai.BackColor = Color.Red;
                groupBox1.Enabled = b;
                btnList.Enabled = b;
            }
            else
            {
                //btnLayMoTa.Enabled = true;
                //btnList.Enabled = true;
                groupBox1.Enabled = b;
                btnList.Enabled = b;
            }
        }
        private static string CleanifyBrowserPath(string p)
        {
            string[] url = p.Split('"');
            string clean = url[1];
            return clean;
        }
        private void txtUrlSanPham_TextChanged(object sender, EventArgs e)
        {
            //if(txtUrlSanPham.Text=="")
            //{
            //    enable_button(false);
            //}
            //else
            //{
            //    enable_button(true);
            //}
        }

        void DongChuongTrinh()
        {
            excel.Close();
            try
            {
                driver.Close();
            }
            catch{}
            finally { driver.Quit(); }
            Environment.Exit(1);
        }

        void open_web_driver()
        {
            driver.Navigate().GoToUrl("https://www.listia.com/list");
            driver.Manage().Window.Size = new System.Drawing.Size(700, 700);
            driver.FindElement(By.XPath("//*[@id='login']")).SendKeys(tk);
            driver.FindElement(By.XPath("//*[@id='password']")).SendKeys(pass);
            driver.FindElement(By.XPath("//*[@id='header_and_contents']/div[4]/div/div[1]/form/div[7]/button")).Click();

        }

        private void Form_dang_nhap_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(t =>
            {
                try
                {
                    open_web_driver();
                }
                catch
                {
                    MessageBox.Show("Chrome bị lỗi. Hãy chạy lại chương trình.");
                    DongChuongTrinh();
                }

            })
            { IsBackground = true };
            thread.Start();

            cboCata1.SelectedIndex = 0;
            chkAutoUpload.Checked = true;
            enable_button(false);
            rdoTuTinh.Checked = true;
            lblDoDaiMoTa.Text = mau_mota.Count().ToString() + "/2000";
            if (mau_mota.Count() > 2000)
            {
                lblDoDaiMoTa.ForeColor = Color.Red;
            }
            else
            {
                lblDoDaiMoTa.ForeColor = Color.Green;
            }
            btnGet.Enabled = false;
        }

        private void btnPast_Click(object sender, EventArgs e)
        {
            if (!Clipboard.GetText().Contains("ebay.com/itm"))
            {
                txtUrlSanPham.Text = "";
                enable_button(false);
                btnGet.Enabled = false;
            }
            else
            {
                txtUrlSanPham.Text = Clipboard.GetText();
                string raw_url = txtUrlSanPham.Text;
                string url = "";
                if (raw_url.IndexOf('?') != -1)
                {
                    url = raw_url.Substring(0, raw_url.IndexOf('?'));
                }
                else url = raw_url;
                txtUrlSanPham.Text = url;
                btnGet.Enabled = true;
            }

            //enable_button(true);
            //addGiabid_Ngaybid();
            //addCategory();

        }


        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                Thread thread1 = new Thread(t1 =>
                {
                    driver.Navigate().GoToUrl("https://www.listia.com/list");
                })
                { IsBackground = true };
                thread1.Start();

                this.Invoke((MethodInvoker)delegate
                {
                    lblTrangThai.Text = "Đang lấy...";
                    lblTrangThai.BackColor = Color.Yellow;
                });
                HamLayThongTin();
                enable_button(true);
            }
            catch
            {
                this.Invoke((MethodInvoker)delegate
                {
                    lblTrangThai.Text = "Chrome lỗi";
                    lblTrangThai.BackColor = Color.Red;
                    enable_button(false);
                });
            }


        }

        private void rdoNhapTay_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNhapTay.Checked)
            {
                txtGiaBan_NhapTay.Enabled = true;
                txtGiaBan_Nhan.Enabled = false;
                txtGiaBan_TuTinh.Enabled = false;
                lblGiaBan_Ketqua_Nhan.Text = "0";
                txtGiaBan_NhapTay.Text = "";
            }
        }

        private void rdoTuTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTuTinh.Checked)
            {
                txtGiaBan_NhapTay.Enabled = false;
                txtGiaBan_Nhan.Enabled = false;
                txtGiaBan_TuTinh.Enabled = true;
                lblGiaBan_Ketqua_Nhan.Text = "0";
            }

        }

        private void rdoNhan_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNhan.Checked)
            {
                txtGiaBan_NhapTay.Enabled = false;
                txtGiaBan_Nhan.Enabled = true;
                txtGiaBan_TuTinh.Enabled = false;
                TinhGia_Nhan();
            }
        }

        void TinhGia_Nhan()
        {
            float nhan;
            float giagoc = StringTofloat(lblGiaban_Tong_Nhan.Text);
            if (txtGiaBan_Nhan.Text != "")
                nhan = StringTofloat(txtGiaBan_Nhan.Text);
            else
                nhan = 1;
            lblGiaban_Tong_Nhan.Text = txtTong.Text;
            lblGiaBan_Ketqua_Nhan.Text = ((float)Math.Round((double)(giagoc * nhan), 2)).ToString().Replace(",", ".");
        }
        private void txtTieuDe_TextChanged(object sender, EventArgs e)
        {
            lblDoDaiTieuDe.Text = txtTieuDe.TextLength.ToString() + "/100";
            if (txtTieuDe.TextLength <= 100 && txtTieuDe.TextLength > 0)
            {
                lblDoDaiTieuDe.ForeColor = Color.Green;
                //add_Tieude_Listia();
            }
            else
            {
                lblDoDaiTieuDe.ForeColor = Color.Red;
            }
        }

        private void txtGiaBan_Nhan_TextChanged(object sender, EventArgs e)
        {
            TinhGia_Nhan();
        }

        private void txtGiaBan_NhapTay_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form_dang_nhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            //DongChuongTrinh();
        }

        private void rtxtMota_TextChanged(object sender, EventArgs e)
        {
            int dodai = mau_mota.Count() + rtxtMota.TextLength;
            lblDoDaiMoTa.Text = dodai.ToString() + "/2000";
            if (dodai <= 2000 && dodai >= 0)
            {
                lblDoDaiMoTa.ForeColor = Color.Green;
            }
            else
            {
                lblDoDaiMoTa.ForeColor = Color.Red;
            }
        }


        void autoupload()
        {
            //DirectoryInfo dir = new DirectoryInfo("anh");
            int i = 1;
            var sortedFiles = new DirectoryInfo("anh").GetFiles()
                                                  .OrderBy(f => f.LastWriteTime)
                                                  .ToList();

            foreach (FileInfo fi in sortedFiles)
            {
                //MessageBox.Show(fi.FullName);
                driver.FindElement(By.XPath(string.Format("//*[@id='photo-list']/li[{0}]/div/div[2]/input", i++))).SendKeys(fi.FullName);
                //driver.FindElement(By.XPath("//*[@id='photo-list']/li[1]/div/div[2]/input")).SendKeys(fi.FullName);
            }
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            Thread main_thread = new Thread(t =>
            {
                //try
                //{
                string mota = "";
                string tieude = "";
                string ngaybid = "3";
                string giabid = "1";

                this.Invoke((MethodInvoker)delegate
                {
                    tieude = txtTieuDe.Text;
                    mota = rtxtMota.Text;
                    ngaybid = txtngaybid.Text;
                    giabid = txtGiabid.Text;
                    lblThongBao.Text = "Chờ...";
                    lblThongBao.BackColor = Color.Yellow;
                });

                if (tieude.Length > 100)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblThongBao.Text = "Tiêu đề quá dài (>100 kí tự)";
                        lblThongBao.BackColor = Color.Red;
                    });
                    return;
                }

                if (tieude.Length <= 0)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblThongBao.Text = "Tiêu đề trống";
                        lblThongBao.BackColor = Color.Red;
                    });
                    return;
                }

                if ((mau_mota.Length + mota.Length) > 2000)
                {
                    this.Invoke((MethodInvoker)delegate
                       {
                           lblThongBao.Text = "Mô tả quá dài (>2000 kí tự)";
                           lblThongBao.BackColor = Color.Red;
                       });
                    return;
                }
                if ((mau_mota.Length + mota.Length) <= 0)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblThongBao.Text = "Mô tả trống";
                        lblThongBao.BackColor = Color.Red;
                    });
                    return;
                }

                if (!driver.Url.Contains("listia.com/list"))
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblThongBao.Text = "Vui lòng chờ website.";
                        lblThongBao.BackColor = Color.Yellow;
                    });
                    return;
                }

                add_Tieude_Listia(tieude);
                if (chkAutoUpload.Checked)
                {
                    autoupload();
                }
                add_Mota_Listia(mota);

                this.Invoke((MethodInvoker)delegate
                {
                try
                    {
                        if (driver.FindElement(By.XPath("//*[@id='auction_form']/div[5]/div/div/div[2]/div[2]/table/tbody/tr/td/span")).Text.Contains("Click to select a category"))
                        {
                            addCategory();
                        }
                    }
                    catch
                    {

                    }
                    
                    if (rdoTuTinh.Checked)
                    {
                        addtienShip(txtGiaBan_TuTinh.Text);
                    }
                    if (rdoNhan.Checked)
                    {
                        addtienShip(lblGiaBan_Ketqua_Nhan.Text);
                    }
                    if (rdoNhapTay.Checked)
                    {
                        if (txtGiaBan_NhapTay.Text != "")
                            addtienShip(txtGiaBan_NhapTay.Text);
                        else
                        {
                            MessageBox.Show("Giá nhập tay không được để trống");
                        }
                    }
                });
                addGiabid_Ngaybid(giabid, ngaybid);
                driver.FindElement(By.XPath("//div[@class='form_float']")).Click();
                try
                {
                    while (!driver.Url.Contains("listia.com/auction"))
                    {
                        try
                        {
                            if (driver.FindElement(By.XPath("//*[contains(@id,'flash-wrapper')]")).Displayed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    lblThongBao.Text = driver.FindElement(By.XPath("//*[contains(@id,'flash-wrapper')]/div/div")).Text;
                                    lblThongBao.BackColor = Color.Yellow;
                                });
                                return;
                            }
                        }
                        catch
                        {

                        }
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: "+ex);
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblThongBao.Text = "Lỗi";
                        lblThongBao.BackColor = Color.Red;
                    });
                    return;
                }
               

                Export_To_Excel(driver.Url);

                this.Invoke((MethodInvoker)delegate
                {
                    lblThongBao.Text = "Thành công";
                    lblThongBao.BackColor = Color.LightGreen;
                });
            })
            { IsBackground = true };
            main_thread.Start();
            btnList.Enabled = false;
        }

        /*ADD VÀO LISTIA*/
        void add_Tieude_Listia(string tieude)
        {
            driver.FindElement(By.Id("auction_form_auction_params_title")).SendKeys(tieude);
        }

        private void Form_dang_nhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DongChuongTrinh();
        }

        private void txtGiaBan_NhapTay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
            && !char.IsDigit(e.KeyChar)
            && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtGiaBan_Nhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
            && !char.IsDigit(e.KeyChar)
            && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtGiabid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
           && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cboCata1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboCata1.SelectedIndex)
            {
                case 0:
                    cboCata2.Items.Clear();
                    cboCata2.Text = "";
                    cboCata3.Text = "";
                    break;
                case 1:
                    cboCata2.Items.Clear();
                    cboCata2.Text = "";
                    cboCata3.Text = "";
                    break;
                case 2:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Paintings");
                    cboCata2.Items.Add("Photography");
                    cboCata2.Items.Add("Posters");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;

                case 3:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Clothes");
                    cboCata2.Items.Add("Diapers");
                    cboCata2.Items.Add("Gear");
                    cboCata2.Items.Add("Toys");
                    cboCata2.Items.Add("Others");
                    cboCata2.SelectedIndex = 0;
                    break;

                case 4:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Children");
                    cboCata2.Items.Add("Cookbooks");
                    cboCata2.Items.Add("Fiction");
                    cboCata2.Items.Add("Nonfiction");
                    cboCata2.Items.Add("Nonfiction");
                    cboCata2.Items.Add("Textbooks & Education");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;
                case 5:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Accessories");
                    cboCata2.Items.Add("Camcorders");
                    cboCata2.Items.Add("Digital Cameras");
                    cboCata2.Items.Add("Memory & Storage");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;
                case 6:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Accessories");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;
                case 7:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Accessories");
                    cboCata2.Items.Add("Cases");
                    cboCata2.Items.Add("Phones");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;
                case 8:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Boys");
                    cboCata2.Items.Add("Girls");
                    cboCata2.Items.Add("Handbags");
                    cboCata2.Items.Add("Men");
                    cboCata2.Items.Add("Shoes");
                    cboCata2.Items.Add("Wallet & Accessories");
                    cboCata2.Items.Add("Women");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;
                case 9:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Coins");
                    cboCata2.Items.Add("Comics");
                    cboCata2.Items.Add("Stamps");
                    cboCata2.Items.Add("Toys");
                    cboCata2.Items.Add("Trading Cards");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;
                case 10:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Components");
                    cboCata2.Items.Add("Desktop & Laptops");
                    cboCata2.Items.Add("Drives & Storage");
                    cboCata2.Items.Add("Keyboard & Mice");
                    cboCata2.Items.Add("Software");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;
                case 11:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Beading & Jewelry Supplies");
                    cboCata2.Items.Add("Crochet");
                    cboCata2.Items.Add("Knitting");
                    cboCata2.Items.Add("Keyboard & Mice");
                    cboCata2.Items.Add("Needlecraft");
                    cboCata2.Items.Add("Needlepoint");
                    cboCata2.Items.Add("Scrapbooking & Paper Crafts");
                    cboCata2.Items.Add("Sewing");
                    cboCata2.Items.Add("Stickers");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;
                case 12:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("DVD & Blu-ray Players");
                    cboCata2.Items.Add("Music Players & Accessories");
                    cboCata2.Items.Add("TVs");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;
                case 13:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Fragrances");
                    cboCata2.Items.Add("Hair");
                    cboCata2.Items.Add("Healthcare Goods");
                    cboCata2.Items.Add("Makeup");
                    cboCata2.Items.Add("Skincare, Bath & Body");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;

                case 14:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Birthday");
                    cboCata2.Items.Add("Christmas");
                    cboCata2.Items.Add("Halloween");
                    cboCata2.Items.Add("Wedding");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;
                case 15:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Decor");
                    cboCata2.Items.Add("Furniture");
                    cboCata2.Items.Add("Gardening");
                    cboCata2.Items.Add("Kitchen");
                    cboCata2.Items.Add("Office Supplies");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;

                case 16:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Body");
                    cboCata2.Items.Add("Bracelets");
                    cboCata2.Items.Add("Charms");
                    cboCata2.Items.Add("Earrings");
                    cboCata2.Items.Add("Necklaces");
                    cboCata2.Items.Add("Rings");
                    cboCata2.Items.Add("Sets");
                    cboCata2.Items.Add("Watches");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;

                case 17:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Blu-ray");
                    cboCata2.Items.Add("DVD");
                    cboCata2.Items.Add("VHS");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;

                case 18:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("CDs");
                    cboCata2.Items.Add("Instruments & Accessories");
                    cboCata2.Items.Add("Records");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;

                case 19:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Bird");
                    cboCata2.Items.Add("Cat");
                    cboCata2.Items.Add("Dog");
                    cboCata2.Items.Add("Fish");
                    cboCata2.Items.Add("Reptile");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;

                case 20:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Baseball");
                    cboCata2.Items.Add("Basketball");
                    cboCata2.Items.Add("Camping & Hunting");
                    cboCata2.Items.Add("Fishing");
                    cboCata2.Items.Add("Fitness");
                    cboCata2.Items.Add("Football");
                    cboCata2.Items.Add("Golf");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;

                case 21:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Building Toys");
                    cboCata2.Items.Add("Cards");
                    cboCata2.Items.Add("Cars & Trains");
                    cboCata2.Items.Add("Dolls & Stuffed Animals");
                    cboCata2.Items.Add("Games");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;

                case 22:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Accessories");
                    cboCata2.Items.Add("Consoles");
                    cboCata2.Items.Add("Games");
                    cboCata2.Items.Add("Prepaid Cards & Codes");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;

                case 23:
                    cboCata2.Items.Clear();
                    cboCata2.Items.Add("Bitcoin");
                    cboCata2.Items.Add("Gift Cards");
                    cboCata2.Items.Add("Rewards Points");
                    cboCata2.Items.Add("Other");
                    cboCata2.SelectedIndex = 0;
                    break;

            }


        }

        private void cboCata2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboCata1.SelectedIndex == 8)
            {
                if (cboCata2.SelectedIndex == 3)
                {
                    cboCata3.Items.Clear();
                    cboCata3.Items.Add("Bottoms");
                    cboCata3.Items.Add("Outerwear");
                    cboCata3.Items.Add("Tops");
                    cboCata3.Items.Add("Other");
                    cboCata3.SelectedIndex = 0;
                }
                else if (cboCata2.SelectedIndex == 6)
                {
                    cboCata3.Items.Clear();
                    cboCata3.Items.Add("Bottoms");
                    cboCata3.Items.Add("Outerwear");
                    cboCata3.Items.Add("Tops");
                    cboCata3.Items.Add("Other");
                    cboCata3.SelectedIndex = 0;
                }
                else
                {
                    cboCata3.Items.Clear();
                    cboCata3.Text = "";
                }
            }
            else if (cboCata1.SelectedIndex == 9)
            {
                if (cboCata2.SelectedIndex == 4)
                {
                    cboCata3.Items.Clear();
                    cboCata3.Items.Add("Gaming");
                    cboCata3.Items.Add("Sports");
                    cboCata3.Items.Add("Other");
                    cboCata3.SelectedIndex = 0;
                }
                else
                {
                    cboCata3.Items.Clear();
                    cboCata3.Text = "";
                }
            }
            else if (cboCata1.SelectedIndex == 13)
            {
                if (cboCata2.SelectedIndex == 1)
                {
                    cboCata3.Items.Clear();
                    cboCata3.Items.Add("Products");
                    cboCata3.Items.Add("Tools & Accessories");
                    cboCata3.Items.Add("Other");
                    cboCata3.SelectedIndex = 0;
                }
                else if (cboCata2.SelectedIndex == 4)
                {
                    cboCata3.Items.Clear();
                    cboCata3.Items.Add("Eyes");
                    cboCata3.Items.Add("Face");
                    cboCata3.Items.Add("Lips");
                    cboCata3.Items.Add("Nails");
                    cboCata3.Items.Add("Tools & Accessories");
                    cboCata3.Items.Add("Other");
                    cboCata3.SelectedIndex = 0;
                }
                else
                {
                    cboCata3.Items.Clear();
                    cboCata3.Text = "";
                }
            }
            else if (cboCata1.SelectedIndex == 15)
            {
                if (cboCata2.SelectedIndex == 2)
                {
                    cboCata3.Items.Clear();
                    cboCata3.Items.Add("Live Plants");
                    cboCata3.Items.Add("Seeds & Bulbs");
                    cboCata3.Items.Add("Tools");
                    cboCata3.Items.Add("Other");
                    cboCata3.SelectedIndex = 0;
                }
                else
                {
                    cboCata3.Items.Clear();
                    cboCata3.Text = "";
                }
            }
            else if (cboCata1.SelectedIndex == 22)
            {
                if (cboCata2.SelectedIndex == 1)
                {
                    cboCata3.Items.Clear();
                    cboCata3.Items.Add("Nintendo");
                    cboCata3.Items.Add("PlayStation");
                    cboCata3.Items.Add("Xbox");
                    cboCata3.Items.Add("Other");
                    cboCata3.SelectedIndex = 0;
                }
                else if (cboCata2.SelectedIndex == 2)
                {
                    cboCata3.Items.Clear();
                    cboCata3.Items.Add("Nintendo");
                    cboCata3.Items.Add("PC");
                    cboCata3.Items.Add("PlayStation");
                    cboCata3.Items.Add("Xbox");
                    cboCata3.Items.Add("Other");
                    cboCata3.SelectedIndex = 0;
                }
                else
                {
                    cboCata3.Items.Clear();
                    cboCata3.Text = "";
                }
            }
            else
            {
                cboCata3.Items.Clear();
                cboCata3.Text = "";
            }

        }

        private void txtTienCong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
            && !char.IsDigit(e.KeyChar)
            && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        void add_Mota_Listia(string mota)
        {
            try
            {
                string chuoi = mota + "\n" + mau_mota;
                chuoi = HttpUtility.JavaScriptStringEncode(chuoi);
                //MessageBox.Show(chuoi);
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("document.getElementById('auction_form_auction_params_description').value='" + chuoi + "';");


            }
            catch
            {
                this.Invoke((MethodInvoker)delegate
                {
                    lblThongBao.Text = "Mô tả không hợp lệ";
                    lblThongBao.BackColor = Color.Red;
                });
            }

        }

        private void cboCata2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtSoAnhCanLay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
          && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        void addtienShip(string ship)
        {
            driver.FindElement(By.Id("auction_form_shipping")).Click();
            driver.FindElement(By.XPath("//a[@class='label-container']")).Click();
            driver.FindElements(By.XPath("//div[@class='dropdown-container']//ul//li"))[1].Click();
            driver.FindElement(By.XPath("//div[@class='input-container']//input")).SendKeys(ship);
        }

        private void rtxtMota_KeyDown(object sender, KeyEventArgs e)
        {

        }

        void addGiabid_Ngaybid(string giabid, string ngaybid)
        {
            driver.FindElement(By.XPath("//a[@class='advanced-options-toggler hide-on-toggle']")).Click();
            driver.FindElement(By.XPath("//div[@class='input-wrapper']//input")).SendKeys(giabid);
            foreach (var a in driver.FindElements(By.XPath("//div[@class='section-item']//select//option")))
            {
                if (a.GetAttribute("value").Equals(ngaybid))
                {
                    a.Click();
                    break;
                }
            }
        }

        void Export_To_Excel(string link_listia)
        {
            //this.Invoke((MethodInvoker)delegate
            //{
            //});            
            Thread thread = new Thread(t =>
            {
                List<string> arr = new List<string>();
                this.Invoke((MethodInvoker)delegate
                {
                    arr.Add(txtTieuDe.Text);
                    arr.Add(link_listia);
                    if (rdoTuTinh.Checked)
                    {
                        arr.Add(txtGiaBan_TuTinh.Text);
                    }
                    if (rdoNhan.Checked)
                    {
                        arr.Add(lblGiaBan_Ketqua_Nhan.Text);
                    }
                    if (rdoNhapTay.Checked)
                    {
                        arr.Add(txtGiaBan_NhapTay.Text);
                    }
                    arr.Add(txtUrlSanPham.Text);
                    arr.Add(txtGiaGoc_USD.Text);
                    arr.Add(txtGiaShip_USD.Text);
                    arr.Add(GetNowDate());
                });

                int i = excel.EmptyCellatColsLocation(0);
                excel.WriteToCell(i, 0, i.ToString());
                excel.addListToCell(i, 1, arr);
                excel.Save();
            })
            { IsBackground = true };
            thread.Start();
        }

        string GetNowDate()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, hanoi).ToString("dd/MM/yyyy");
        }
        void addCategory()
        {
            if (cboCata1.SelectedIndex != 0)
            {
                driver.FindElement(By.XPath("//div[@class='selection-display']")).Click();
                driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']//ul//li"))[cboCata1.SelectedIndex - 1].Click();
                //Xử lý trong đây
                if (cboCata1.SelectedIndex != 1)
                {
                    switch (cboCata1.SelectedIndex)
                    {
                        case 2:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[1].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 3:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[2].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 4:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[3].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 5:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[4].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 6:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[5].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 7:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[6].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 8:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[7].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            if (cboCata2.SelectedIndex == 3)
                            {
                                foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[8].FindElements(By.XPath("//ul//li")))
                                {
                                    if (a.Text == cboCata3.Text)
                                    {
                                        a.Click();
                                        break;
                                    }
                                }
                            }
                            if (cboCata2.SelectedIndex == 6)
                            {
                                foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[9].FindElements(By.XPath("//ul//li")))
                                {
                                    if (a.Text == cboCata3.Text)
                                    {
                                        a.Click();
                                        break;
                                    }
                                }
                            }
                            break;
                        case 9:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[10].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            if (cboCata2.SelectedIndex == 4)
                            {
                                foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[11].FindElements(By.XPath("//ul//li")))
                                {
                                    if (a.Text == cboCata3.Text)
                                    {
                                        a.Click();
                                        break;
                                    }
                                }
                            }
                            break;
                        case 10:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[12].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 11:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[13].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 12:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[14].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 13:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[15].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            if (cboCata2.SelectedIndex == 1)
                            {
                                foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[16].FindElements(By.XPath("//ul//li")))
                                {
                                    if (a.Text == cboCata3.Text)
                                    {
                                        a.Click();
                                        break;
                                    }
                                }
                            }
                            if (cboCata2.SelectedIndex == 3)
                            {
                                foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[17].FindElements(By.XPath("//ul//li")))
                                {
                                    if (a.Text == cboCata3.Text)
                                    {
                                        a.Click();
                                        break;
                                    }
                                }
                            }
                            break;
                        case 14:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[18].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 15:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[19].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            if (cboCata2.SelectedIndex == 2)
                            {
                                foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[20].FindElements(By.XPath("//ul//li")))
                                {
                                    if (a.Text == cboCata3.Text)
                                    {
                                        a.Click();
                                        break;
                                    }
                                }
                            }
                            break;
                        case 16:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[21].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 17:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[22].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 18:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[23].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 19:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[24].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 20:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[25].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 21:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[26].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;
                        case 22:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[27].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            if (cboCata2.SelectedIndex == 1)
                            {
                                foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[28].FindElements(By.XPath("//ul//li")))
                                {
                                    if (a.Text == cboCata3.Text)
                                    {
                                        a.Click();
                                        break;
                                    }
                                }
                            }
                            if (cboCata2.SelectedIndex == 2)
                            {
                                foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[29].FindElements(By.XPath("//ul//li")))
                                {
                                    if (a.Text == cboCata3.Text)
                                    {
                                        a.Click();
                                        break;
                                    }
                                }
                            }
                            break;
                        case 23:
                            foreach (var a in driver.FindElements(By.XPath("//div[@class='drilldown-select-menu']//div[@class='page']"))[30].FindElements(By.XPath("//ul//li")))
                            {
                                if (a.Text == cboCata2.Text)
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            break;


                    }
                }
            }
        }
    }



}
