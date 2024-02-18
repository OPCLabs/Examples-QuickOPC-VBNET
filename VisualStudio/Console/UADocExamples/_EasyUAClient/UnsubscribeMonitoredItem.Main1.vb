﻿' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how unsubscribe from changes of a single monitored item.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-OpcStudio/Latest/examples.html .

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace _EasyUAClient
    Friend Class UnsubscribeMonitoredItem
        Public Shared Sub Main1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object and hook events
            Dim client = New EasyUAClient()
            AddHandler client.DataChangeNotification, AddressOf client_DataChangeNotification

            Console.WriteLine("Subscribing...")
            Dim handle As Integer =
                client.SubscribeDataChange(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000)

            Console.WriteLine("Processing monitored item changed events for 10 seconds...")
            Threading.Thread.Sleep(10 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeMonitoredItem(handle)

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)
        End Sub

        Private Shared Sub client_DataChangeNotification(ByVal sender As Object, ByVal e As EasyUADataChangeNotificationEventArgs)
            ' Display value
            If e.Succeeded Then
                Console.WriteLine("Value: {0}", e.AttributeData.Value)
            Else
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief)
            End If
        End Sub
    End Class
End Namespace

#End Region