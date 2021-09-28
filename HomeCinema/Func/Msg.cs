using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace HomeCinema
{
    public static class Msg
    {
        public static void ShowNoParent(string msg, string caption = "HomeCinema")
        {
            var form = new frmAlert(msg, caption, 0, null, HCIcons.None);
            form.TopMost = true;
            form.ShowDialog();
            form?.Dispose();
        }
        public static void ShowCustomMessage(string msg, string caption, Form parent, HCIcons icon)
        {
            try
            {
                Form caller = (parent == null) ? Program.FormMain : parent;
                if (Program.FormMain == null)
                {
                    ShowNoParent(msg, caption);
                    return;
                }
                if (caller == Program.FormMain)
                {
                    if (Program.FormMain.InvokeRequired)
                    {
                        Program.FormMain.BeginInvoke((Action)delegate
                        {
                            var form = new frmAlert(msg, caption, 0, caller, icon);
                            form.ShowDialog(caller);
                        });
                    }
                    else
                    {
                        var form = new frmAlert(msg, caption, 0, caller, icon);
                        form.ShowDialog(caller);
                    }
                }
                else
                {
                    if (caller.InvokeRequired)
                    {
                        caller.BeginInvoke((Action)delegate
                        {
                            var form = new frmAlert(msg, caption, 0, caller, icon);
                            form.ShowDialog(caller);
                        });
                    }
                    else
                    {
                        var form = new frmAlert(msg, caption, 0, caller, icon);
                        form.ShowDialog(caller);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.LogErr("GlobalVars-ShowInfo", ex);
                ShowNoParent(msg, caption);
            }
        }
        public static void ShowInfo(string msg, string caption = "", Form parent = null)
        {
            ShowCustomMessage(msg, caption, parent, HCIcons.Info);
        }
        public static void ShowWarning(string msg, string caption = "", Form parent = null)
        {
            ShowCustomMessage(msg, caption, parent, HCIcons.Warning);
        }
        public static void ShowError(string codeFrom, string msg, Form parent, Exception error, bool openLog)
        {
            Logs.LogErr(codeFrom, error);
            bool ShowAMsg = (!String.IsNullOrWhiteSpace(msg));
            if (ShowAMsg)
            {
                string message = "An error occured!";
                ShowCustomMessage($"{message}\nReport on project site\nand submit 'logs' subfolder.", "Error occured!", parent, HCIcons.Error);
                // Open file in explorer
                if (openLog)
                {
                    try { Process.Start("explorer.exe", DataFile.PATH_LOG); }
                    catch { }
                }
            }
        }
        public static void ShowError(string codeFrom, Exception error)
        {
            ShowError(codeFrom, "", null, error, false);
        }
        public static void ShowError(string codeFrom, Exception error, string msg)
        {
            ShowError(codeFrom, msg, null, error, false);
        }
        public static void ShowError(string codeFrom, Exception error, string msg, Form parent)
        {
            ShowError(codeFrom, msg, parent, error, false);
        }
        public static bool ShowYesNo(string msg, Form caller = null)
        {
            try
            {
                if (caller == null) { caller = Program.FormMain; }
                return (new frmAlert(msg, GlobalVars.CAPTION_DIALOG, 1, caller, HCIcons.Question).ShowDialog(caller) == DialogResult.Yes);
            }
            catch (Exception ex)
            {
                ShowError("GlobalVars-ShowYesNo", ex, "Cannot show 'Yes/No' prompt!\nTry again.");
            }
            return false;
        }

        // Get string from InputBox
        public static string GetStringInputBox(string caption, string defaultVal)
        {
            var form = new frmInputBox(caption, null, defaultVal);
            form.ShowDialog();
            string value = (!String.IsNullOrWhiteSpace(form.Result)) ? form.Result.Trim() : String.Empty;
            form.Dispose();
            return value;
        }
        public static string GetStringInputBox(List<string> items, List<string> vals, string caption)
        {
            var form = new frmInputBox(caption, items, "");
            form.Values = vals;
            form.ShowDialog();
            string value = (!String.IsNullOrWhiteSpace(form.Result)) ? form.Result.Trim() : String.Empty;
            form.Dispose();
            return value;
        }
    }
}
