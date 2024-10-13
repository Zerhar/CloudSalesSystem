# Cloud Sales System

## Overview

The Cloud Sales System is a web API developed using .NET Core that allows customers to buy and manage software solutions offered by a Cloud Computing Provider (CCP). The system enables users to create and manage accounts, view available software services, order software licenses, and manage purchased software licenses.

## Features

- **List Accounts:** Retrieve a list of accounts associated with a customer.
- **Available Software Services:** Get a list of software services available from the Cloud Computing Provider (mocked).
- **Order Software License:** Place an order for software licenses for a specific account.
- **View Purchased Licenses:** See all purchased software licenses for a specific account.
- **Change License Quantity:** Update the quantity of licenses for a purchased software.
- **Cancel Software License:** Remove a specific software license for any account.
- **Extend License Validity:** Update the "valid to" date for a software license.

## Technology Stack

- **Backend:** .NET 8 (ASP.NET Core)
- **Database:** SQL Server
- **ORM:** Entity Framework Core

## Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/Zerhar/CloudSalesSystem.git
   cd CloudSalesSystem
