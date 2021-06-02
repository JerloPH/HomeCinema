https://github.com/kakone/GoogleCast

https://www.csharp-examples.net/inputbox/

https://stackoverflow.com/questions/3097364/c-sharp-form-close-vs-form-dispose#:~:text=Close()%20sends%20the%20proper,the%20form%20is%20holding%20onto.
snippet:

Form.Close() sends the proper Windows messages to shut down the win32 window. During that process, if the form was not shown modally, Dispose is called on the form. Disposing the form frees up the unmanaged resources that the form is holding onto.

If you do a form1.Show() or Application.Run(new Form1()), Dispose will be called when Close() is called.

However, if you do form1.ShowDialog() to show the form modally, the form will not be disposed, and you'll need to call form1.Dispose() yourself. I believe this is the only time you should worry about disposing the form yourself.