﻿using ICanPay.Alipay;
using ICanPay.Alipay.Domain;
using ICanPay.Alipay.Request;
using ICanPay.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ICanPay.Demo.Controllers
{
    public class AlipayController : Controller
    {
        private readonly BaseGateway _baseGateway;

        public AlipayController(IGateways gateways)
        {
            _baseGateway = gateways.Get<AlipayGateway>();
        }

        [HttpPost]
        public IActionResult WebPay(string out_trade_no, string subject, double total_amount, string body)
        {
            var request = new WebPayRequest();
            request.AddGatewayData(new WebPayModel()
            {
                Body = body,
                TotalAmount = total_amount,
                Subject = subject,
                OutTradeNo = out_trade_no,
            });

            var response = _baseGateway.Execute(request);
            return Content(response.Html, "text/html", Encoding.UTF8);
        }

        //[HttpPost]
        //public async Task<IActionResult> WapPay(string out_trade_no, string subject, string total_amount, string body, string product_code, string notify_url, string return_url)
        //{
        //    var model = new AlipayTradeWapPayModel()
        //    {
        //        Body = body,
        //        Subject = subject,
        //        TotalAmount = total_amount,
        //        OutTradeNo = out_trade_no,
        //        ProductCode = product_code,
        //    };
        //    var req = new AlipayTradeWapPayRequest();
        //    req.SetBizModel(model);
        //    req.SetNotifyUrl(notify_url);
        //    req.SetReturnUrl(return_url);

        //    var response = await _client.PageExecuteAsync(req, null, "GET");
        //    return Redirect(response.Body);
        //}

        //[HttpPost]
        //public async Task<IActionResult> PreCreate(string out_trade_no, string subject, string total_amount, string body, string notify_url)
        //{
        //    var model = new AlipayTradePrecreateModel()
        //    {
        //        Body = body,
        //        Subject = subject,
        //        TotalAmount = total_amount,
        //        OutTradeNo = out_trade_no,
        //    };
        //    var req = new AlipayTradePrecreateRequest();
        //    req.SetBizModel(model);
        //    req.SetNotifyUrl(notify_url);

        //    var response = await _client.ExecuteAsync(req);
        //    return Ok(response.Body);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Pay(string out_trade_no, string scene, string auth_code, string subject, string total_amount, string body, string notify_url)
        //{
        //    var model = new AlipayTradePayModel()
        //    {
        //        Scene = scene,
        //        AuthCode = auth_code,
        //        Body = body,
        //        Subject = subject,
        //        TotalAmount = total_amount,
        //        OutTradeNo = out_trade_no,
        //    };
        //    var req = new AlipayTradePayRequest();
        //    req.SetBizModel(model);
        //    req.SetNotifyUrl(notify_url);

        //    var response = await _client.ExecuteAsync(req);
        //    return Ok(response.Body);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Query(string out_trade_no, string trade_no)
        //{
        //    var model = new AlipayTradeQueryModel()
        //    {
        //        OutTradeNo = out_trade_no,
        //        TradeNo = trade_no
        //    };

        //    var req = new AlipayTradeQueryRequest();
        //    req.SetBizModel(model);

        //    var response = await _client.ExecuteAsync(req);
        //    return Ok(response.Body);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Refund(string out_trade_no, string trade_no, string refund_amount, string refund_reason, string out_request_no)
        //{
        //    var model = new AlipayTradeRefundModel()
        //    {
        //        OutTradeNo = out_trade_no,
        //        TradeNo = trade_no,
        //        RefundAmount = refund_amount,
        //        OutRequestNo = out_request_no,
        //        RefundReason = refund_reason
        //    };

        //    var req = new AlipayTradeRefundRequest();
        //    req.SetBizModel(model);

        //    var response = await _client.ExecuteAsync(req);
        //    return Ok(response.Body);
        //}

        //[HttpPost]
        //public async Task<IActionResult> RefundQuery(string out_trade_no, string trade_no, string out_request_no)
        //{
        //    var model = new AlipayTradeFastpayRefundQueryModel()
        //    {
        //        OutTradeNo = out_trade_no,
        //        TradeNo = trade_no,
        //        OutRequestNo = out_request_no
        //    };

        //    var req = new AlipayTradeFastpayRefundQueryRequest();
        //    req.SetBizModel(model);

        //    var response = await _client.ExecuteAsync(req);
        //    return Ok(response.Body);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Close(string out_trade_no, string trade_no)
        //{
        //    var model = new AlipayTradeCloseModel()
        //    {
        //        OutTradeNo = out_trade_no,
        //        TradeNo = trade_no,
        //    };

        //    var req = new AlipayTradeCloseRequest();
        //    req.SetBizModel(model);

        //    var response = await _client.ExecuteAsync(req);
        //    return Ok(response.Body);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Trans(string out_biz_no, string payee_account, string payee_type, string amount, string remark)
        //{
        //    var model = new AlipayFundTransToaccountTransferModel()
        //    {
        //        OutBizNo = out_biz_no,
        //        PayeeType = payee_type,
        //        PayeeAccount = payee_account,
        //        Amount = amount,
        //        Remark = remark
        //    };
        //    var req = new AlipayFundTransToaccountTransferRequest();
        //    req.SetBizModel(model);
        //    var response = await _client.ExecuteAsync(req);
        //    return Ok(response.Body);
        //}

        //[HttpPost]
        //public async Task<IActionResult> TransQuery(string out_biz_no, string order_id)
        //{
        //    var model = new AlipayFundTransOrderQueryModel()
        //    {
        //        OutBizNo = out_biz_no,
        //        OrderId = order_id,
        //    };

        //    var req = new AlipayFundTransOrderQueryRequest();
        //    req.SetBizModel(model);
        //    var response = await _client.ExecuteAsync(req);
        //    return Ok(response.Body);
        //}

        //[HttpPost]
        //public async Task<IActionResult> BillDownloadurlQuery(string bill_date, string bill_type)
        //{
        //    var model = new AlipayDataDataserviceBillDownloadurlQueryModel()
        //    {
        //        BillDate = bill_date,
        //        BillType = bill_type
        //    };

        //    var req = new AlipayDataDataserviceBillDownloadurlQueryRequest();
        //    req.SetBizModel(model);
        //    var response = await _client.ExecuteAsync(req);
        //    return Ok(response.Body);
        //}

        //[HttpGet]
        //public async Task<IActionResult> PagePayReturn()
        //{
        //    try
        //    {
        //        var notify = await _notifyClient.ExecuteAsync<AlipayTradePagePayReturnResponse>(Request);
        //        return Content("success", "text/plain");
        //    }
        //    catch
        //    {
        //        return Content("error", "text/plain");
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> WapPayReturn()
        //{
        //    try
        //    {
        //        var notify = await _notifyClient.ExecuteAsync<AlipayTradeWapPayReturnResponse>(Request);
        //        return Content("success", "text/plain");
        //    }
        //    catch
        //    {
        //        return Content("error", "text/plain");
        //    }
        //}
    }
}
