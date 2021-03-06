using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text;
using com.unionpay.acp.sdk;

namespace upacp_demo_wtz.demo.api_03_wtz
{
    public partial class FrontRcvResponse : System.Web.UI.Page
    {
        protected string html;

        protected void Page_Load(object sender, EventArgs e)
        {

            log4net.ILog log = log4net.LogManager.GetLogger(this.GetType());

            if (Request.HttpMethod == "GET")
            {
                // 无跳转产品的前台开通失败会get方式请求，不带报文数据。
                // 其他产品不会get方式访问，不用处理get的内容。
                html = "开通失败";
                log.Info("get to front notify. ");
            }
            // **************演示前台接收银联返回报文交易结果展示***********************
            else if (Request.HttpMethod == "POST")
            {
                // 使用Dictionary保存参数
                Dictionary<string, string> resData = new Dictionary<string, string>();

                NameValueCollection coll = Request.Form;

                string[] requestItem = coll.AllKeys;

                for (int i = 0; i < requestItem.Length; i++)
                {
                    resData.Add(requestItem[i], Request.Form[requestItem[i]]);
                }

                //商户端根据返回报文内容处理自己的业务逻辑 ,DEMO此处只输出报文结果
                StringBuilder builder = new StringBuilder();
                log.Info("receive front notify: " + SDKUtil.CreateLinkString(resData, false, true, System.Text.Encoding.UTF8));

                builder.Append("<tr><td align=\"center\" colspan=\"2\"><b>商户端接收银联返回报文并按照表格形式输出结果</b></td></tr>");

                for (int i = 0; i < requestItem.Length; i++)
                {
                    builder.Append("<tr><td width=\"30%\" align=\"right\">" + requestItem[i] + "</td><td style='word-break:break-all'>" + Request.Form[requestItem[i]] + "</td></tr>");
                }

                if (AcpService.Validate(resData, System.Text.Encoding.UTF8))
                {
                    builder.Append("<tr><td width=\"30%\" align=\"right\">商户端验证银联返回报文结果</td><td>验证签名成功.</td></tr>");

                    string respcode = resData["respCode"]; //00、A6为成功，其余为失败。其他字段也可按此方式获取。

                    //如果卡号我们业务配了会返回且配了需要加密的话，请按此方法解密
                    //if(resData.ContainsKey("accNo"))
                    //{
                    //    string accNo = SecurityUtil.DecryptData(resData["accNo"], System.Text.Encoding.UTF8); 
                    //}

                    //customerInfo子域的获取
                    if (resData.ContainsKey("customerInfo"))
                    {
                        Dictionary<string, string> customerInfo = AcpService.ParseCustomerInfo(resData["customerInfo"], System.Text.Encoding.UTF8);
                        if (customerInfo.ContainsKey("phoneNo"))
                        {
                            string phoneNo = customerInfo["phoneNo"]; //customerInfo其他子域均可参考此方式获取  
                        }
                        foreach (KeyValuePair<string, string> pair in customerInfo)
                        {
                            builder.Append(pair.Key + "=" + pair.Value + "<br>\n");
                        }
                    }
                }
                else
                {
                    builder.Append("<tr><td width=\"30%\" align=\"right\">商户端验证银联返回报文结果</td><td>验证签名失败.</td></tr>");
                }
                html = builder.ToString();
            }
        }
    }
}
