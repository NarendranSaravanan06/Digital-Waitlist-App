using System.ComponentModel;

public class WaitListModel : INotifyPropertyChanged
{
    public int SNo { get; set; }
    public int Id { get; set; }

    private string name;
    private string email;
    private string phoneNo;
    private string status;

    public string Name
    {
        get => name;
        set { name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(IsModified)); }
    }

    public string Email
    {
        get => email;
        set { email = value; OnPropertyChanged(nameof(Email)); OnPropertyChanged(nameof(IsModified)); }
    }

    public string PhoneNo
    {
        get => phoneNo;
        set { phoneNo = value; OnPropertyChanged(nameof(PhoneNo)); OnPropertyChanged(nameof(IsModified)); }
    }

    public string Status
    {
        get => status;
        set { status = value; OnPropertyChanged(nameof(Status)); OnPropertyChanged(nameof(IsModified)); }
    }

    public string OriginalName { get; set; }
    public string OriginalEmail { get; set; }
    public string OriginalPhoneNo { get; set; }
    public string OriginalStatus { get; set; }

    public bool IsModified =>
        Name != OriginalName ||
        Email != OriginalEmail ||
        PhoneNo != OriginalPhoneNo ||
        Status != OriginalStatus;

    public void SetOriginalValues()
    {
        OriginalName = Name;
        OriginalEmail = Email;
        OriginalPhoneNo = PhoneNo;
        OriginalStatus = Status;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
