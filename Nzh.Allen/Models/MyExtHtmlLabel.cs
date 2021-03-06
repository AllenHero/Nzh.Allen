﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Allen.Models
{
    public static class MyExtHtmlLabel
    {
        public static HtmlString SearchBtnHtml(this IHtmlHelper helper, string title = "搜索", string _class = "")
        {
            return new HtmlString(string.Format(@"<button class='layui-btn {1}' lay-submit='' lay-filter='search'>
                                                    <i class='layui-icon'>&#xe615;</i>{0}
                                                </button>", title, _class));
        }

        public static HtmlString ResetBtnHtml(this IHtmlHelper helper, string title = "重置", string _class = "layui-btn-primary")
        {
            return new HtmlString(string.Format(@"<button type='reset' id='reset' class='layui-btn {1}'>{0}</button>", title, _class));
        }

        public static HtmlString RightToolBarHtml(this IHtmlHelper helper, dynamic _list = null)
        {
            StringBuilder sb = new StringBuilder();
            var list = _list as List<ButtonModel>;
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    sb.AppendLine(string.Format(@"<a class='layui-btn layui-btn-xs {0}' href='javascript:;' lay-event='{1}' id='{1}'><i class='ok-icon'>{3}</i>{2}</a>", item.ClassName, item.EnCode, item.FullName, item.Icon));
                }
            }
            return new HtmlString(sb.ToString());
        }

        public static HtmlString TopToolBarHtml(this IHtmlHelper helper, dynamic _list = null)
        {
            StringBuilder sb = new StringBuilder();
            var list = _list as List<ButtonModel>;
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    sb.AppendLine(string.Format(@"<button class='layui-btn layui-btn-sm {0}' lay-event='{1}' id='{1}'><i class='ok-icon'>{3}</i>{2}</button>", item.ClassName, item.EnCode, item.FullName, item.Icon));
                }
            }
            return new HtmlString(sb.ToString());
        }

        public static HtmlString EnabledMarkSelectHtml(this IHtmlHelper helper, string defaultTxt = "")
        {
            return new HtmlString(string.Format(@"<div class='layui-input-inline'>
                                                    <select name='EnabledMark'>
                                                        <option value=''>{0}</option>
                                                        <option value='0'>启用</option >
                                                        <option value='1'>禁用</option >
                                                    </select>
                                                </div>", defaultTxt));
        }

        public static HtmlString GenderRadioHtml(this IHtmlHelper helper, int defaultVal = 1)
        {
            var male = defaultVal == 1 ? "checked" : "";
            var female = defaultVal == 0 ? "checked" : "";
            return new HtmlString(string.Format(@"<div class='layui-form-item' pane>
                                        <label class='layui-form-label'>性别</label>
                                        <div class='layui-input-block'>
                                            <input type='radio' name='Gender' value='1' title='男' {0}>
                                            <input type='radio' name='Gender' value='0' title='女' {1}>
                                        </div>
                                    </div>", male, female));
        }

        public static HtmlString EnabledMarkRadioHtml(this IHtmlHelper helper, int defaultVal = 0)
        {
            var enabled = defaultVal == 0 ? "checked" : "";
            var disabled = defaultVal == 1 ? "checked" : "";
            return new HtmlString(string.Format(@"<div class='layui-form-item' pane>
                                        <label class='layui-form-label'>状态</label>
                                        <div class='layui-input-block'>
                                            <input type='radio' name='EnabledMark' value='0' title='开启' {0}>
                                            <input type='radio' name='EnabledMark' value='1' title='禁用' {1}>
                                        </div>
                                    </div>", enabled, disabled));
        }
    }
}
