using System;
using System.ComponentModel;

public class WaitListModel : INotifyPropertyChanged
{
    public int SNo { get; set; }
    public int Id { get; set; }

    private string name;
    private string email;
    private string phoneNo;
    private string status;
    private TimeSpan inTime;
    private TimeSpan? outTime;
    private DateTime date;

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

    public TimeSpan InTime
    {
        get => inTime;
        set { inTime = value; OnPropertyChanged(nameof(InTime)); OnPropertyChanged(nameof(IsModified)); }
    }

    public TimeSpan? OutTime
    {
        get => outTime;
        set { outTime = value; OnPropertyChanged(nameof(OutTime)); OnPropertyChanged(nameof(IsModified)); }
    }

    public DateTime Date
    {
        get => date;
        set { date = value; OnPropertyChanged(nameof(Date)); OnPropertyChanged(nameof(IsModified)); }
    }

    // Track original values for comparison
    public string OriginalName { get; set; }
    public string OriginalEmail { get; set; }
    public string OriginalPhoneNo { get; set; }
    public string OriginalStatus { get; set; }
    public TimeSpan OriginalInTime { get; set; }
    public TimeSpan? OriginalOutTime { get; set; }
    public DateTime OriginalDate { get; set; }

    public bool IsModified =>
        Name != OriginalName ||
        Email != OriginalEmail ||
        PhoneNo != OriginalPhoneNo ||
        Status != OriginalStatus ||
        InTime != OriginalInTime ||
        OutTime != OriginalOutTime ||
        Date != OriginalDate;

    public void SetOriginalValues()
    {
        OriginalName = Name;
        OriginalEmail = Email;
        OriginalPhoneNo = PhoneNo;
        OriginalStatus = Status;
        OriginalInTime = InTime;
        OriginalOutTime = OutTime;
        OriginalDate = Date;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
