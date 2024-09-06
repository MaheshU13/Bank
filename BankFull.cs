using System;
using System.Collections;

class BankAccount
{
    private string name;
    private int accountNum;
    private int type;
    private float balance;
    private float dep;
    private float wd;

    public int AccountNum
    {
        get { return accountNum; }
        set { accountNum = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public BankAccount(string name, int accountNum, int type, float balance)
    {
        this.name = name;
        this.accountNum = accountNum;
        this.type = type;
        this.balance = balance;
        this.dep = 0;
        this.wd = 0;
    }

    public void Deposit()
    {
        System.Console.Write("Enter Deposit Amount : ");
        dep = float.Parse(System.Console.ReadLine());
        balance += dep;
        System.Console.WriteLine("Deposit successful.");
    }

    public void Withdraw()
    {
        System.Console.Write("Enter Amount to Withdraw : ");
        wd = float.Parse(System.Console.ReadLine());
        if (wd > balance)
        {
            System.Console.WriteLine("Insufficient Balance!");
            wd = 0;
        }
        else
        {
            balance -= wd;
            System.Console.WriteLine("Withdrawal successful.");
        }
    }

    public void Display()
    {
        System.Console.WriteLine("\n\n\t**************************************************");
        System.Console.WriteLine("\t\tName: " + name);
        System.Console.WriteLine("\t\tAccount number: " + accountNum);
        System.Console.WriteLine("\t\tAccount type (1: Savings, 2: Current): " + type);
        System.Console.WriteLine("\t\tAmount deposited: " + dep);
        System.Console.WriteLine("\t\tAmount withdrawn: " + wd);
        System.Console.WriteLine("\t\tFinal Balance: " + balance);
    }

    public void DeleteAccount()
    {
        // Reset
        name = "";
        accountNum = 0;
        type = 0;
        balance = 0;
        dep = 0;
        wd = 0;
        System.Console.WriteLine("Account deleted successfully.");
    }
}

class Bank
{
    static ArrayList accounts = new ArrayList();

    public static void Main()
    {
        System.Console.WriteLine("---------------WELCOME TO THE BANK ------------------\n\n");

        // Test accounts
        InitializeDummyAccounts();

        bool exit = false;
        while (!exit)
        {
            System.Console.WriteLine("Select Login Type:");
            System.Console.WriteLine("1. Admin");
            System.Console.WriteLine("2. Customer");
            System.Console.WriteLine("3. Exit");
            System.Console.Write("Enter your choice: ");
            int choice = int.Parse(System.Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AdminMenu();
                    break;

                case 2:
                    CustomerMenu();
                    break;

                case 3:
                    exit = true;
                    System.Console.WriteLine("Exiting program. Goodbye!");
                    break;

                default:
                    System.Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                    break;
            }
        }
    }

    static void AdminMenu()
    {
        System.Console.WriteLine("\n--- Admin Login ---");
        bool adminExit = false;
        while (!adminExit)
        {
            System.Console.WriteLine("\nAdmin Options:");
            System.Console.WriteLine("1. Create Account");
            System.Console.WriteLine("2. Delete Account");
            System.Console.WriteLine("3. View All Accounts");
            System.Console.WriteLine("4. Back to Main Menu");
            System.Console.Write("Enter your choice: ");
            int adminChoice = int.Parse(Console.ReadLine());

            switch (adminChoice)
            {
                case 1:
                    CreateAccount();
                    break;

                case 2:
                    DeleteAccount();
                    break;

                case 3:
                    ViewAllAccounts();
                    break;

                case 4:
                    adminExit = true;
                    break;

                default:
                    System.Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    break;
            }
        }
    }

    static void CustomerMenu()
    {
        System.Console.WriteLine("\n--- Customer Login ---");
        System.Console.Write("Enter your account number: ");
        int accNum = int.Parse(System.Console.ReadLine());

        BankAccount account = FindAccount(accNum);

        if (account == null)
        {
            System.Console.WriteLine("Account not found. Please contact the bank.");
            return;
        }

        bool customerExit = false;
        while (!customerExit)
        {
            System.Console.WriteLine("\nCustomer Options:");
            System.Console.WriteLine("1. Deposit");
            System.Console.WriteLine("2. Withdraw");
            System.Console.WriteLine("3. Balance Enquiry");
            System.Console.WriteLine("4. Back to Main Menu");
            System.Console.Write("Enter your choice: ");
            int customerChoice = int.Parse(System.Console.ReadLine());

            switch (customerChoice)
            {
                case 1:
                    account.Deposit();
                    break;

                case 2:
                    account.Withdraw();
                    break;

                case 3:
                    account.Display();
                    break;

                case 4:
                    customerExit = true;
                    break;

                default:
                    System.Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    break;
            }
        }
    }

    static void CreateAccount()
    {
        System.Console.Write("Please enter the customer's name: ");
        string name = System.Console.ReadLine();

        System.Console.Write("Please enter a unique 9-digit account number: ");
        int accountNum = int.Parse(System.Console.ReadLine());

        if (FindAccount(accountNum) != null)
        {
            System.Console.WriteLine("Account number already exists. Please try another.");
            return;
        }

        System.Console.Write("Please enter the account type (1 for Savings, 2 for Current): ");
        int type = int.Parse(System.Console.ReadLine());
        while (type != 1 && type != 2)
        {
            System.Console.Write("\aInvalid Input! Please enter 1 for Savings or 2 for Current: ");
            type = int.Parse(System.Console.ReadLine());
        }

        System.Console.Write("Please enter the initial balance: ");
        float balance = float.Parse(Console.ReadLine());

        BankAccount newAccount = new BankAccount(name, accountNum, type, balance);
        accounts.Add(newAccount);
        System.Console.WriteLine("Account created successfully.");
    }

    static void DeleteAccount()
    {
        System.Console.Write("Enter account number to delete: ");
        int accNumToDelete = int.Parse(System.Console.ReadLine());

        BankAccount accToDelete = FindAccount(accNumToDelete);
        if (accToDelete != null)
        {
            accToDelete.DeleteAccount();
        }
        else
        {
            System.Console.WriteLine("Account with number {0} not found.", accNumToDelete);
        }
    }

    static void ViewAllAccounts()
    {
        System.Console.WriteLine("\n--- Viewing All Accounts ---");
        foreach (BankAccount acc in accounts)
        {
            if (acc != null && !string.IsNullOrEmpty(acc.Name))
            {
                acc.Display();
            }
        }
    }

    static void InitializeDummyAccounts()
    {

        BankAccount dummyAccount1 = new BankAccount("Mahesh", 123456789, 1, 150000);
        accounts.Add(dummyAccount1);

        BankAccount dummyAccount2 = new BankAccount("Sahil", 987654321, 2, 180000);
        accounts.Add(dummyAccount2);

	BankAccount dummyAccount3 = new BankAccount("Vishal", 012345678, 3, 260000);
        accounts.Add(dummyAccount3);
    }

    static BankAccount FindAccount(int accountNum)
    {
        foreach (BankAccount acc in accounts)
        {
            if (acc != null && acc.AccountNum == accountNum)
            {
                return acc;
            }
        }
        return null;
    }
}
