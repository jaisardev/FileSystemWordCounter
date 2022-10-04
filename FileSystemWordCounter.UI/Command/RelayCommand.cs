using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileSystemWordCounter.UI.Command
{
  class RelayCommand : ICommand
  {
    private Action<object> _action;
    public RelayCommand(Action<object> action)
    {
      _action = action;
    }
    #region ICommand Members  
    public bool CanExecute(object parameter)
    {
      return true;
    }
    public event EventHandler CanExecuteChanged;
    public void Execute(object action)
    {
      if (action != null)
      {
        _action(action);
      }
    }
    #endregion
  }
}
