# Clinical API

Brief description of what the project does and its purpose.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)
- [Conventional Commits](#conventional_commits)

## Overview

Provide a detailed description of the project. Explain the problem it solves, its goals, and any relevant context.

## Features

- List of features or functionalities of the project.
- Include any unique selling points or key benefits.

## Installation

### Prerequisites

- Ensure you have a [GitHub account](https://github.com/join). If not, [sign up](https://github.com/join) for one.
- [Git](https://git-scm.com/) installed on your machine.
- [.NET SDK](https://dotnet.microsoft.com/download) installed (version X.X or higher).

### Login to GitHub

1. **Open Terminal or Command Prompt**

2. **Authenticate with GitHub**

   Use the following command to authenticate with GitHub:

   ```bash
   gh auth login

### Setup
1. **Clone the Repository**

   ```bash
   git clone https://github.com/LuuHuynhVinhPhuc/Clinical-backend.git
   cd your-repository

2. **Install Dependencies**
   ```bash
   dotnet restore

3. **Build Project**
   ```bash
   dotnet restore

## Conventional Commits

To maintain consistency and clarity in commit messages, we use the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) standard at D2iQ. This convention helps in formulating structured and readable commit messages.

### Commit Message Format

A commit message should follow this structure:

   ```text
   <type>[optional scope]: <description>

   [optional body]

   [optional footer(s)]
   ```

### Commit Types

- **feat**: Introduces a new feature.
- **fix**: Fixes a bug.
- **chore**: Changes that do not affect src or test files, like updating dependencies.
- **refactor**: Refactors code without fixing bugs or adding features.
- **docs**: Updates to documentation.
- **style**: Code formatting changes that do not affect the meaning.
- **test**: Includes or corrects tests.
- **perf**: Performance improvements.
- **ci**: Continuous integration related changes.
- **build**: Changes affecting the build system or external dependencies.
- **revert**: Reverts a previous commit.

### Guidelines

- **Subject Line**: Should be lowercase and concise.
- **Body**: Use it to provide additional details that cannot fit in the subject line.
- **Footer**: Optional, used to reference issues or link to JIRA stories (e.g., `Closes D2IQ-12345`).

### Example

   ```text
   fix: fix foo to enable bar

   This fixes the broken behavior of the component by doing xyz.
   
   BREAKING CHANGE
   Before this fix foo wasn't enabled at all, behavior changes from <old> to <new>
   
   Closes D2IQ-12345
   ```

