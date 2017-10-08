<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ClassroomBackend.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
          <meta charset="utf-8" />
          <base href="/" />

          <meta name="viewport" content="width=device-width, initial-scale=1" />
          <link rel="icon" type="image/x-icon" href="favicon.ico" />

    <title>Student Checkin</title>
</head>
<body>

<app-root slist='<%=StudentList %>'></app-root>

  <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.4.min.js"></script>
  <script src="http://ajax.aspnetcdn.com/ajax/signalr/jquery.signalr-2.2.2.min.js"></script>
<script src="http://localhost:55199/signalr/hubs"></script>

<script type="text/javascript" src="inline.bundle.js"></script><script type="text/javascript" src="polyfills.bundle.js"></script><script type="text/javascript" src="styles.bundle.js"></script><script type="text/javascript" src="vendor.bundle.js"></script><script type="text/javascript" src="main.bundle.js"></script>
</body>
</html>
