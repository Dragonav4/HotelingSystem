namespace Hoteling.Application.Views.Common;

public class ActionListView<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int Actions { get; set; }
}
