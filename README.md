# SortSmart: Book Sorting Application

SortSmart assists users in learning and practicing book sorting based on the Dewey Decimal System. With an interactive interface, users can drag and drop books to reorder them and verify their sorting skills.

## Table of Contents
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Running the Application](#running-the-application)
- [Usage](#usage)
- [Contact](#contact)

## Getting Started
These instructions will guide you on how to get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites
1. **Visual Studio** - Preferably Visual Studio 2019 or later. You can download it from the official Visual Studio website.
2. **.NET Framework 4.7.2** or later should be installed. You can download it from Microsoft's official site.

### Installation
1. Clone the repository to your local machine using the following command:
git clone https://github.com/hannahsmith7/SortSmart.git
2. Navigate to the project directory:
cd SortSmart
3. Open the solution file (`SortSmart.sln`) in Visual Studio.
4. Restore any missing NuGet packages. In Visual Studio, right-click on the solution in the Solution Explorer and select "Restore NuGet Packages".
5. Build the solution either by pressing `CTRL + SHIFT + B` or via the menu: `Build > Build Solution`.

## Running the Application
1. Ensure the main project (probably named `SortSmart`) is set as the startup project in Visual Studio. If not, right-click on it in the Solution Explorer and select "Set as Startup Project".
2. Press `F5` or click the "Start" button in Visual Studio to run the application.

## Usage
1. On launch, choose from the available tasks: "Replacing Books" or "Identifying Areas".
2. For the "Replacing Books" task:
   - You'll see a list of call numbers representing books.
   - Drag and drop these call numbers to reorder them.
   - Use the "Check Order" button to verify the order.
   - The "Shuffle Books" button shuffles the order of books.
   - A progress bar indicates your correctness level in ordering the books.
3. For the "Identifying Areas" task:
   - You'll see two columns: one with call numbers and the other with book descriptions.
   - Match the call numbers with their corresponding descriptions by dragging and dropping.
   - Each correct match will increment your score, providing instant feedback.
   - Continue matching until all items are correctly paired.

## Contact
- **Student:** Hannah Smith
- **Git Repository:** [SortSmart on GitHub](https://github.com/hannahsmith7/SortSmart.git)

