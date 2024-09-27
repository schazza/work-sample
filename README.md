# Scharolta's Work Sample

## Overview

This program processes file paths and file contents based on the following assumptions:

1. **Input**: The program accepts exactly **one** input parameter, which can be either:

   - A file name with an extension.
   - A file path, including the directory.

2. **Matching Logic**: The program identifies and counts matching substrings. However:

   - **Overlapping substrings** are not counted. For example, the string `fifififi` will match the substring `fifi` **twice**, not three times.

3. **File Type Handling**: There are currently no restrictions on file extensions. Depending on intended usage of this program this could be a future improvement.
