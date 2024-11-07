# People Management System

This project is a console application written in **VB.NET** that allows users to manage a list of people. Users can add, list, filter, and sort people based on various criteria, such as age, name initials, and more. Additionally, the system supports saving and loading data from a file asynchronously.

## Features

The application provides the following features:

1. **Add Person**: Add a new person with their name, age, and city to the system.
2. **List People**: Display all people currently stored in the system.
3. **Filter People by Age Range**: Filter and display people within a specified age range.
4. **Sort People by Age**: Sort the people by their age in ascending order and display them.
5. **Calculate Age Statistics**: Calculate and display statistics like the average age, the oldest person, and the youngest person.
6. **Search by Name Initial**: Search for people whose name starts with a specific character.
7. **Save Data to File (Async)**: Save the list of people to a file asynchronously.
8. **Load Data from File (Async)**: Load the list of people from a file asynchronously.
9. **Exit**: Exit the program.

## Usage

When the program is run, it will present a menu with options to the user. The user can choose an option by entering a number corresponding to one of the available actions.

### Example Workflow

1. The user is prompted to **Add Person**:
    - The user is asked to input the person's **name**, **age**, and **city**.
    - The data is validated, and if correct, the person is added to the system.

2. The user can choose to **List People**, which will display all the records in the system.

3. If the user wants to filter people by age, they can choose the **Filter People by Age Range** option. They will be asked for a **minimum** and **maximum** age.

4. The program can also **Sort People by Age** or **Calculate Age Statistics** like average age, youngest person, and oldest person.

5. The **Search by Name Initial** option allows the user to find people whose names start with a specific character.

6. The user can **Save Data to File (Async)** or **Load Data from File (Async)** by providing a file name. The data will be saved or loaded asynchronously.

7. The user can choose to **Exit** the program when done.

## Installation

To run this application, follow these steps:

1. Ensure you have **Visual Studio** or any **.NET Framework** compatible IDE installed.
2. Clone or download this repository to your local machine.
3. Open the project in your IDE and build the solution.
4. Run the application.

## Dependencies

- **VB.NET** (Visual Basic .NET) is required to compile and run this application.
- The application uses standard **System.IO** and **System.Linq** namespaces for file operations and LINQ functionality.

## Code Structure

The project consists of the following main files:

- **Program.vb**: The entry point of the application. It contains the main menu and calls various database functions based on user input.
- **Person.vb**: Contains the `Person` class, which defines the properties `Name`, `Age`, and `City` for each person.
- **Database.vb**: Manages the list of people and provides methods to add, list, filter, sort, save, and load data.

## Example Menu

```text
Select an option:
1. Add Person
2. List People
3. Filter People by Age Range
4. Sort People by Age
5. Calculate Age Statistics
6. Search by Name Initial
7. Save Data to File (Async)
8. Load Data from File (Async)
9. Exit
