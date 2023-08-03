using System;

public class Students
{
    static int counter = 500;
    public string ID { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string gender { get; set; }

    public Students()
    {
        counter += 1;
        ID = "";
        first_name= "";
        last_name = "";
        gender = "";
    }
}
