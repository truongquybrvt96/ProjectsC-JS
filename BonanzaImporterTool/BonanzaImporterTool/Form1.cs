using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Collections.Specialized;
using Jurassic.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Xml;
using System.Threading;
using System.Web.Script.Serialization;
namespace BonanzaImporterTool
{
    public partial class Form1 : Form
    {
        WebBrowser browser = new WebBrowser();
        string deleteme = "";
        FieldSelectingForm FSF1;
        List<string> listOfLinks = new List<string>();
        public Form1()
        {
            InitializeComponent();
            FSF1 = new FieldSelectingForm();
        }

        private void btnSelFields_Click(object sender, EventArgs e)
        {
            FSF1.ShowDialog();
        }

        private void btnToCSVFile_Click(object sender, EventArgs e)
        {
           /* Thread tToCSVButton = new Thread(()=>
            {*/
                /*try
                {*/
                    lblStatus.Invoke((MethodInvoker)(() =>
                    {
                        lblStatus.ForeColor = Color.DarkOrange;
                        lblStatus.Text = "Processing...";
                    }));
                    int errorsCount = 0;
                    int successCount = 0;
                    int listOfLinksCount = listOfLinks.Count;
                    StringBuilder csv = new StringBuilder();
                    List<string> namesTempList = new List<string>();
                    foreach (CheckBox chk in FSF1.Controls.Find("grbSFS_Fields", true).First().Controls.OfType<CheckBox>().OrderBy(c => c.TabIndex))
                    {
                        namesTempList.Add(chk.Text);
                    }
                    namesTempList.Add("alie_id");
                    string fieldString = string.Join(",", namesTempList);
                    csv.AppendLine(fieldString);
                    for (int i = 0; i < listOfLinksCount; i++)
                    {
                        int processOL_code = ProcessOneLink(listOfLinks.ElementAt(i), ref csv, namesTempList);
                        if (processOL_code != 0)
                        {
                            errorsCount++;
                        }
                        successCount++;
                        this.Invoke((MethodInvoker)(() =>
                        {
                            lblProcess.Text = successCount + "/" + listOfLinksCount;
                        }));

                    }
                    /*DateTime date = DateTime.Now;

                    string filenameOutput = date.Day.ToString("00") + "_" + date.Month.ToString("00") + "_" + date.Year + " " + date.Hour.ToString("00") + "_" + date.Minute.ToString("00") + "_" + date.Second.ToString("00");
                    if (lblOutputFolderPath.Text == "none")
                    {
                        File.WriteAllText(Directory.GetCurrentDirectory() + @"\data\" + filenameOutput + ".csv", csv.ToString());
                    }
                    else
                    {
                        File.WriteAllText(lblOutputFolderPath.Text + @"\" + filenameOutput + ".csv", csv.ToString());
                    }
                    

                    lblStatus.Invoke((MethodInvoker)(() =>
                    {
                        lblStatus.ForeColor = Color.DarkGreen;
                        lblStatus.Text = "Done!";
                    }));*/
                /*}
                catch(Exception ex)
                {
                    MessageBox.Show("Write to CSV file throws an exeption: " + ex.InnerException);
                }*/
           /* });
            tToCSVButton.Start();*/
            
        }

        private int ProcessOneLink(string _link, ref StringBuilder _csv, List<string> _namesTempList)
        {
            string haha = _link;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlWeb htWeb = new HtmlWeb();
            htWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";
            doc = htWeb.Load(_link);
            haha += " " + doc.DocumentNode.OuterHtml;
            Clipboard.SetText(haha);
            if(doc == null)
            {
                MessageBox.Show("Error loading document. Please check the internet connection!");
                return -6;
            }
            NameValueCollection record = new NameValueCollection();

            List<string> images = new List<string>();

            List<String_String> name_and_des_title_img = new List<String_String>();
            ProductInfoSku PIS = new ProductInfoSku();
            int getPIS_code = GetProductInfoSku(doc, ref PIS, ref images, ref name_and_des_title_img);
            if(getPIS_code != 0)
            {
                MessageBox.Show("GetProductInfoSku returns code: " + getPIS_code);
                return -3;
            }
            List<string> namesOfSku = PIS.GetAllNames();

            //Description and brand
            HtmlNode desMasterNode = doc.DocumentNode.SelectSingleNode("//*[@id='j-product-desc']/div[1]/div[2]/ul");
            string des = ""; //+ Transfer.moreDescriptionHead + "\n";
            string brandName = "Unbranded";
            foreach (HtmlNode desChilds in desMasterNode.Elements("li"))
            {
                if (desChilds.InnerText.Contains("Brand Name:"))
                {
                    brandName = desChilds.Elements("span").Last().InnerText;
                }
                des += Regex.Replace(desChilds.InnerText.Replace("\n", ""), @"\s+", " ") + "\n";
            }
            des = des.Replace("\n", "<br>");
            des += "<table style='width:100%' border='5'>";
            for (int _i = 0; _i < name_and_des_title_img.Count; _i++)
            {
                des += name_and_des_title_img.ElementAt(_i).name2; // name_and_des_title_img.Get(_i) 
            }
            des += "</table><br>";
            record.Add("description", des);
            //Title
            string titleTextField = doc.DocumentNode.SelectSingleNode("//*[@id='j-product-detail-bd']/div[1]/div/h1/text()").InnerText;
            titleTextField = titleTextField.Replace(brandName, "");
            record.Add("title", titleTextField);



            HtmlNode scriptNode = doc.DocumentNode.Descendants().Where(n => n.Name == "script" && n.InnerText.Contains("skuProducts")).First();
            string skuProductsString = "";
            TrimStringByTwoChars('[', ']', scriptNode.InnerText, ref skuProductsString, "skuProducts");
            JArray jsonArray = JArray.Parse(skuProductsString);
            string traitsField = "";



            //Condition
            traitsField += "[[Condition:" + Transfer.conditionStatus + "]]";

            //Weight
            string weight = "0";
            weight = doc.DocumentNode.Descendants("li").Where(n => n.HasClass("packaging-item") && n.InnerText.Contains("Package Weight:")).First().Descendants("span").Where(m => m.InnerText.Contains("kg")).First().Attributes["rel"].Value;
            traitsField += "[[weight:" + weight + "]][[weight_unit:kg]]";

            //trait 
            int totalQtt = 0;
            foreach (JToken jToken in jsonArray)
            {
                string childTrait = "[";
                string skuPropIds = jToken["skuPropIds"].ToString();
                string[] skuIds = skuPropIds.Split(',');
                for (int i = 0; i < skuIds.Count<string>(); i++)
                {
                    childTrait += "[" + namesOfSku.ElementAt(i) + ":" + PIS.proItemList.ElementAt(i).skuList_hasIMGTag.skuList[skuIds.ElementAt(i)] + "]";
                }

                string displayPrice = "";
                //Discount price
                if (rdoDiscountPrice.Checked)
                {
                    if (jToken["skuVal"]["actSkuMultiCurrencyDisplayPrice"] != null)
                    {
                        displayPrice = jToken["skuVal"]["actSkuMultiCurrencyDisplayPrice"].ToString();
                    }
                    else
                    {
                        displayPrice = jToken["skuVal"]["skuMultiCurrencyDisplayPrice"].ToString();
                    }
                }
                if(rdoOriginalPrice.Checked)
                {
                    displayPrice = jToken["skuVal"]["skuMultiCurrencyDisplayPrice"].ToString();
                }

                if(displayPrice == "")
                {
                    MessageBox.Show("Error 153. Cannot continue!");
                    return -10;
                }

                
                PricingMultiply(ref displayPrice);
                childTrait += "[price:" + displayPrice + "]";
                string availQuantity = jToken["skuVal"]["availQuantity"].ToString();
                if (availQuantity == "0" && FSF1.Controls.Find("chkReplaceZeroQtt", false).OfType<CheckBox>().First().Checked)
                {
                    availQuantity = FSF1.Controls.Find("mudReplaceZeroQtt", false).OfType<NumericUpDown>().First().Value.ToString();
                }
                totalQtt += Int32.Parse(availQuantity);
                childTrait += "[quantity:" + availQuantity + "]]";
                traitsField += childTrait;
            }
            record.Add("trait", traitsField);

            //quantity
            record.Add("quantity", totalQtt.ToString());

            //Images
            HtmlNode jImageThumbList = doc.GetElementbyId("j-image-thumb-list");
            IEnumerable<HtmlNode> jImageLis = jImageThumbList.Elements("li");
            foreach (HtmlNode jImageLi in jImageLis)
            {
                string imgWithTail = jImageLi.Element("span").Element("img").Attributes["src"].Value;
                string imgWithoutTail = imgWithTail.Remove(imgWithTail.LastIndexOf('_'));
                images.Add(imgWithoutTail);
            }
            string imagesField = string.Join(" ", images);
            record.Add("images", imagesField);

            //sku and id
            string gaDataString = "";
            TrimStringByTwoChars('{', '}', scriptNode.InnerText, ref gaDataString, "GaData");
            JToken productIds = JToken.Parse(gaDataString)["productIds"];
            string IDandSKUField = "al" + productIds.ToString();
            record.Add("sku", IDandSKUField);
            record.Add("id", IDandSKUField);


            //Cheapest + fastest shipping price. shipping_price, shipping_type, shipping_service, worldwide_shipping_price, worldwide_shipping_type, 
            string baseShippingURL = @"https://freight.aliexpress.com/ajaxFreightCalculateService.htm?count=1&currencyCode=USD&sendGoodsCountry=&country=US";
            string productID = string.Join("", productIds.ToString().Where(char.IsDigit));
            WebClient webClient = new WebClient();
            string sourceString = webClient.DownloadString(baseShippingURL + "&productid=" + productID);

            sourceString = sourceString.Remove(sourceString.IndexOf("("), 1);
            sourceString = sourceString.Remove(sourceString.LastIndexOf(")"));
            JToken shippingJsonRaw = JToken.Parse(sourceString);
            List<ShippingMethod> SMs = new List<ShippingMethod>();
            int cheapestInx = -1;
            double cheapestPrice;

            decimal shortestTimeMean;
            int shortestTimeMeanInx = -1;
            cheapestPrice = Convert.ToInt32(shippingJsonRaw["freight"][0]["price"].ToString());

            string _timeTemp = shippingJsonRaw["freight"][0]["time"].ToString();
            string[] _minMaxTimeTemp = _timeTemp.Split('-');
            shortestTimeMean = (Convert.ToInt32(_minMaxTimeTemp[0]) + Convert.ToInt32(_minMaxTimeTemp[1])) / 2m;


            for (int i = 0; i < shippingJsonRaw["freight"].Count(); i++)
            {
                string _name = shippingJsonRaw["freight"][i]["companyDisplayName"].ToString();
                string _price = shippingJsonRaw["freight"][i]["price"].ToString();
                string _time = shippingJsonRaw["freight"][i]["time"].ToString();
                string[] _minMaxTime = _time.Split('-');
                decimal _timeMean = (Convert.ToInt32(_minMaxTime[0]) + Convert.ToInt32(_minMaxTime[1])) / 2m;
                if (cheapestPrice >= Convert.ToDouble(_price))
                {
                    cheapestInx = i;
                    cheapestPrice = Convert.ToDouble(_price);
                }
                if (shortestTimeMean >= _timeMean)
                {
                    shortestTimeMeanInx = i;
                    shortestTimeMean = _timeMean;
                }
                SMs.Add(new ShippingMethod(_name, Convert.ToDouble(_price), _timeMean));
            }
            List<ShippingMethod> cheapestPricingSMs = new List<ShippingMethod>();
            for (int i = 0; i < SMs.Count; i++)
            {
                if (Convert.ToDouble(SMs.ElementAt(i).price) < cheapestPrice + 4)
                {
                    cheapestPricingSMs.Add(SMs.ElementAt(i));
                }

            }
            int cheapestShortestSMInx = 0;
            ShippingMethod cheapestShortestSM = cheapestPricingSMs.ElementAt(0);

            for (int i = 0; i < cheapestPricingSMs.Count; i++)
            {
                if (cheapestShortestSM.timeMean >= cheapestPricingSMs.ElementAt(i).timeMean)
                {
                    cheapestShortestSMInx = i;
                    cheapestShortestSM = cheapestPricingSMs.ElementAt(i);
                }
            }
            record.Add("shipping_price", cheapestShortestSM.price.ToString());
            record.Add("shipping_type", "flat");
            if (cheapestShortestSM.timeMean > 0 && cheapestShortestSM.timeMean <= 10)
            {
                record.Add("shipping_service", "Economy shipping (1 to 10 business days)");
            }
            else if (cheapestShortestSM.timeMean > 10 && cheapestShortestSM.timeMean <= 22)
            {
                record.Add("shipping_service", "International Shipping (2 to 3 weeks)");
            }
            else if (cheapestShortestSM.timeMean > 22 && cheapestShortestSM.timeMean <= 28)
            {
                record.Add("shipping_service", "International Shipping (3 to 4 weeks)");
            }
            else if (cheapestShortestSM.timeMean > 28 && cheapestShortestSM.timeMean <= 45)
            {
                record.Add("shipping_service", "International Shipping (4 to 5 weeks)");
            }
            else
            {
                record.Add("shipping_service", "Unspecified shipping type");
            }
            record.Add("worldwide_shipping_price", FSF1.Controls.Find("nudWorldwideFlatRate", false).OfType<NumericUpDown>().First().Value.ToString());
            record.Add("worldwide_shipping_type", "flat");



            //alie_link
            record.Add("alie_id", productID);

            //force_update
            record.Add("force_update", "true");

            string newRecordString = "";
            foreach (string _name in _namesTempList)
            {
                newRecordString += "\"" + record[_name] + "\"" + ",";
            }
            _csv.AppendLine(newRecordString);

            return 0;
        }

        public System.Windows.Forms.HtmlDocument GetHtmlDocument(string html)
        {
            WebBrowser browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true;
            browser.DocumentText = html;
            browser.Document.OpenNew(true);
            browser.Document.Write(html);
            browser.Refresh();
            return browser.Document;
        }

        private int GetProductInfoSku(HtmlAgilityPack.HtmlDocument _doc, ref ProductInfoSku PIS, ref List<string> images, ref List<String_String> name_and_des_title_img)
        {
           /* try
            {*/
                HtmlNode jProductInfoSku = _doc.GetElementbyId("j-product-info-sku");
            
                IEnumerable<HtmlNode> proItems = jProductInfoSku.Elements("dl");

                foreach (HtmlNode proItem in proItems)
                {
                    string name = "";
                    //Gonna be added to NameVal_String
                    NameValueCollection skuList = new NameValueCollection();
                    int hasIMGTag = 0;

                    name = proItem.Element("dt").InnerText.Replace(":", "");
                    HtmlAgilityPack.HtmlNode jSkuList = proItem.Element("dd").Element("ul");
                    IEnumerable<HtmlNode> itemSkus = jSkuList.Elements("li");
                    //itemSku: li element contains class item-sku-color
                    foreach (HtmlNode itemSku in itemSkus)
                    {
                        string dataSkuId = itemSku.FirstChild.Attributes["data-sku-id"].Value;
                        string title;
                        if (itemSku.FirstChild.Attributes.Contains("title"))
                        {
                            title = itemSku.FirstChild.Attributes["title"].Value;
                        }
                        else
                        {
                            title = itemSku.FirstChild.InnerText;
                        }
                        skuList.Add(dataSkuId, title);
                        //Images of colors

                        if (itemSku.FirstChild.Descendants("img").Count<HtmlNode>() > 0)
                        {
                            if (itemSku.FirstChild.Element("img").Attributes.Contains("bigpic"))
                            {
                                string bigpicWithTail = itemSku.FirstChild.Element("img").Attributes["bigpic"].Value;
                                string bigpicWithoutTail = bigpicWithTail.Remove(bigpicWithTail.LastIndexOf('_'));
                                images.Add(bigpicWithoutTail);

                                string values_s = "<tr><td>" + name + ": " + title + "</td><td><img width='480' height='480' src='" + bigpicWithTail + "'></td></tr>";
                                name_and_des_title_img.Add(new String_String(name, values_s));
                            }
                            hasIMGTag = 1;

                        }

                        //Background colors
                        else if (itemSku.FirstChild.Descendants("span").Where(n => n.Attributes.Contains("class") && n.Attributes["class"].Value.Contains("sku-color-")).Count<HtmlNode>() > 0)
                        {
                            MessageBox.Show(itemSku.FirstChild.Descendants("span").First().OuterHtml);
                            HtmlNode backgroundColorCSSElem = itemSku.FirstChild.Descendants("span").Where(n => n.Attributes.Contains("class") && n.Attributes["class"].Value.Contains("sku-color-")).First();
                            string className = backgroundColorCSSElem.Attributes["class"].Value;
                            string colorStyle = BackgroundColorSelector(className);
                            if (colorStyle == "")
                                return -2;
                            string values_s = "<tr><td>" + name + ": " + title + "</td><td width='480' height='480' style='" + BackgroundColorSelector(className) + "'></td></tr>";
                            name_and_des_title_img.Add(new String_String(name, values_s));
                        }
                        else if (name.ToLower() == "color")
                        {
                            MessageBox.Show("Color not found! Liên hệ https://www.facebook.com/letruongquy96 \nOuterHTML: " + itemSku.InnerHtml);
                            return -1;
                        }

                    }
                    NameVal_String skuList_hasIMGTag = new NameVal_String(skuList, hasIMGTag);
                    PropertyItem tempPI = new PropertyItem(name, skuList_hasIMGTag);
                    PIS.Add(tempPI);

                }
            /*}
            catch(Exception ex)
            {
                MessageBox.Show("GetProductInfoSku: " + ex.Message);
                return -7;
            }*/
            
            return 0;
        }

        public void TrimStringByTwoChars(char startChar, char endChar, string input, ref string output, string key)
        {
            int indexOfKeyInInput = input.IndexOf(key);
            int indexOfFirstStartChar = input.IndexOf(startChar, indexOfKeyInInput + 1);

            int count = 1;
            for(int i = indexOfFirstStartChar + 1; i < input.ToCharArray().Length; i++)
            {
                if(input.ElementAt<char>(i) == startChar)
                {
                    count++;
                }
                if(input.ElementAt<char>(i) == endChar)
                {
                    count--;
                }
                if(count == 0)
                {
                    output = input.Substring(indexOfFirstStartChar, i - indexOfFirstStartChar + 1);
                    break;
                }
            }
        }

        private void btnPricingRules_Click(object sender, EventArgs e)
        {
            FrmPricingRules FPR = new FrmPricingRules();
            FPR.Show();
        }

        private void PricingMultiply(ref string priceString)
        {
            //Get pricing rules from xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"data\Pricing_Rules.xml");
            XmlNodeList rules = xmlDoc.DocumentElement.ChildNodes;
            decimal price = decimal.Parse(priceString);
            for(int i = 0; i < rules.Count; i++)
            {
                decimal minCR = decimal.Parse(rules[i]["minCR"].InnerText);
                decimal maxCR = decimal.Parse(rules[i]["maxCR"].InnerText);
                decimal markup = decimal.Parse(rules[i]["markup"].InnerText);
                if(price >= minCR && price <= maxCR)
                {
                    price *= markup;
                    priceString = price.ToString();
                    break;
                }
            }
        }

        //Returns background color
        //Input class name. like sku-color-*
        public string BackgroundColorSelector(string className)
        {
            string toReturn = "";

            switch(className)
            {
                case "sku-color-771":
                    toReturn = "background:#F8F7E7!important";
                    break;
                case "sku-color-193":
                    toReturn = "background:#000!important";
                    break;
                case "sku-color-173":
                    toReturn = "background:#0080FF!important";
                    break;
                case "sku-color-365458":
                    toReturn = "background:#8d6468!important";
                    break;
                case "sku-color-200001951":
                    toReturn = "background:#d5b489!important";
                    break;
                case "sku-color-350850":
                    toReturn = "background:gold!important";
                    break;
                case "sku-color-691":
                    toReturn = "background:#999!important";
                    break;
                case "sku-color-175":
                    toReturn = "background:#007000!important";
                    break;
                case "sku-color-200001438":
                    toReturn = "background:#dac9b9!important";
                    break;
                case "sku-color-350852":
                    toReturn = "background:orange!important";
                    break;
                case "sku-color-1052":
                    toReturn = "background:pink!important";
                    break;
                case "sku-color-496":
                    toReturn = "background:#6C3365!important";
                    break;
                case "sku-color-10":
                    toReturn = "background:red!important";
                    break;
                case "sku-color-350853":
                    toReturn = "background:#CCC!important";
                    break;
                case "sku-color-29":
                    toReturn = "background:#FFF!important";
                    break;
                case "sku-color-366":
                    toReturn = "background:#FF0!important";
                    break;
                case "sku-color-200003699":
                    toReturn = "background:url(http://i.alicdn.com/ae-detail-ui/node_modules/@alife/beta-skucolor/sku_color_mutil.b30df4d9.gif) center no-repeat!important";
                    break;
                case "sku-color-100018786":
                    toReturn = "background:url(http://i.alicdn.com/ae-detail-ui/node_modules/@alife/beta-skucolor/sku_color_clear.62374b82.gif) center no-repeat!important";
                    break;
                case "sku-color-200002130":
                    toReturn = "background:#fdfde8!important";
                    break;
                case "sku-color-200004889":
                    toReturn = "background:#7C8C30!important";
                    break;
                case "sku-color-1254":
                    toReturn = "background:#1eddff!important";
                    break;
                case "sku-color-200004890":
                    toReturn = "background:#666!important";
                    break;
                case "sku-color-200004891":
                    toReturn = "background:plum!important";
                    break;
                case "sku-color-4602":
                    toReturn = "background:#B58654!important";
                    break;
                case "sku-color-100010417":
                    toReturn = "background:#0FF!important";
                    break;
                case "sku-color-100016350":
                    toReturn = "background:url(http://is.alicdn.com/wimg/seller/single/bg_post_color_block.gif) center no-repeat";
                    break;
                case "sku-color-200000195":
                    toReturn = "background:#7B3F00!important";
                    break;
                case "sku-color-200000396":
                    toReturn = "background:#FFFDD0!important";
                    break;
                case "sku-color-200002984":
                    toReturn = "background:#900020!important";
                    break;
                case "sku-color-200004870":
                    toReturn = "background:#00008B!important";
                    break;
                case "sku-color-200006151":
                    toReturn = "background:#D3D3D3!important";
                    break;
                case "sku-color-200006152":
                    toReturn = "background:#90EE90!important";
                    break;
                case "sku-color-200006153":
                    toReturn = "background:#FFFFE0!important";
                    break;
                case "sku-color-200006154":
                    toReturn = "background:#BDB76B!important";
                    break;
                case "sku-color-200006156":
                    toReturn = "background:#8E4585!important";
                    break;
                case "sku-color-200006157":
                    toReturn = "background:#00008B!important";
                    break;
                case "sku-color-200013902":
                    toReturn = "background:#4169E1!important";
                    break;
                case "sku-color-200141872":
                    toReturn = "background:#8E4585!important";
                    break;
                case "sku-color-200211869":
                    toReturn = "background:#FE007F!important";
                    break;
                case "sku-color-200844061":
                    toReturn = "background:#D3D3D3!important";
                    break;
                case "sku-color-201619813":
                    toReturn = "background:#006400!important";
                    break;
                case "sku-color-201619814":
                    toReturn = "background:#FFFFE0!important";
                    break;
                case "sku-color-201800840":
                    toReturn = "background:navy!important";
                    break;
                case "sku-color-201967807":
                    toReturn = "background:#FE007F!important";
                    break;
                case "sku-color-202135821":
                    toReturn = "background:orchid!important";
                    break;
                case "sku-color-202430841":
                    toReturn = "background:navy!important";
                    break;
                case "sku-color-202598807":
                    toReturn = "sku-color-202598807";
                    break;
                case "sku-color-203008817":
                    toReturn = "background:#F0F!important";
                    break;
                case "sku-color-202520811":
                    toReturn = "background:#7FFFD4!important";
                    break;
                case "sku-color-202997806":
                    toReturn = "background:coral!important";
                    break;
                case "sku-color-203008818":
                    toReturn = "sku-color-203008818";
                    break;


            }

            return toReturn;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            Thread tLoadTxt = new Thread(()=>
                {
                    try
                    {
                        OpenFileDialog fileDialog = new OpenFileDialog();
                        fileDialog.Filter = "txt file (*.txt)|*.txt";
                        fileDialog.RestoreDirectory = true;
                        if (fileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //lblURL.Text = fileDialog.FileName;
                            lblTxtLoadingStatus.Invoke((MethodInvoker)(() => lblTxtLoadingStatus.Text = "Loading..."));
                            lblTxtLoadingStatus.Invoke((MethodInvoker)(() => lblTxtLoadingStatus.ForeColor = Color.DarkOrange));
                            lblURL.Invoke((MethodInvoker)(() => lblURL.Text = fileDialog.FileName));
                            listOfLinks.Clear();
                            //Process text file
                            string stream = File.ReadAllText(fileDialog.FileName);
                            //lblLinks.Text = stream.Split(' ').Count<string>().ToString();
                            foreach (string singleLink in stream.Split(' '))
                            {
                                if (singleLink.Contains("https://www.aliexpress.com") || singleLink.Contains("//www.aliexpress.com"))
                                {
                                    string singleLinkWithoutPro = singleLink.Remove(0, singleLink.IndexOf("//") + 2);
                                    listOfLinks.Add("https://" + singleLinkWithoutPro);
                                }
                                else if(singleLink.Contains("www.aliexpress.com"))
                                {
                                    listOfLinks.Add("https://" + singleLink);
                                }
                                lblLinks.Invoke((MethodInvoker)(() => lblLinks.Text = listOfLinks.Count.ToString()));
                            }
                            string lele = "";
                            foreach(string list in listOfLinks)
                            {
                                lele += list + "\n";
                            }
                            MessageBox.Show(lele);
                            lblTxtLoadingStatus.Invoke((MethodInvoker)(() => lblTxtLoadingStatus.Text = "Loading done!"));
                            lblTxtLoadingStatus.Invoke((MethodInvoker)(() => lblTxtLoadingStatus.ForeColor = Color.DarkGreen));
                            if (listOfLinks.Count > 0)
                            {
                                btnToCSVFile.Invoke((MethodInvoker)(() => btnToCSVFile.Enabled = true));
                            }
                            else
                            {
                                btnToCSVFile.Invoke((MethodInvoker)(() => btnToCSVFile.Enabled = false));
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Loading file error: " + ex.Message);
                    }
                    
                });
            tLoadTxt.SetApartmentState(ApartmentState.STA);
            tLoadTxt.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fldbd = new FolderBrowserDialog();
            fldbd.ShowNewFolderButton = true;
            if(fldbd.ShowDialog() == DialogResult.OK)
            {
                lblOutputFolderPath.Text = Path.GetFullPath(fldbd.SelectedPath);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate("https://www.aliexpress.com/store/product/New-Arrival-Fashion-Sport-LED-Watch-Candy-Color-Silicone-Rubber-Touch-Screen-Digital-Watches-Waterproof-Bracelet/1263155_32828794397.html");
        }

        protected void AppendParameter(StringBuilder sb, string name, string value)
        {
            string encodedValue = WebUtility.UrlEncode(value);
            sb.AppendFormat("{0}={1}&", name, encodedValue);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            AppendParameter(sb, "loginId", "truongquybrvtad01@gmail.com");
            AppendParameter(sb, "password", "123456789");

            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());

            string url = "https://login.aliexpress.com/"; //or: check where the form goes

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.Credentials = CredentialCache.DefaultNetworkCredentials; // ??

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                MessageBox.Show(responseString);
            }
            // do something with response
        }
    }
}

