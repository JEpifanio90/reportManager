   M S B A M L   `   `   `  ���� I  EReport Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Report_ManagerReport_Manager      Report_Manager.userView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:Report_Manager       � Title$  Create a Request��$��501.464��$��725.61��
�������$��Center=� � WindowStartupLocation$ CenterScreen=�.��� .+��� -    ��Name$ label2��$��Requerimientos��$
��Left=�$��
213,10,0,0q�$	��Top=�$��40(�$��60��$	��289���� -    ��Name$ 
tabControl��$
��Left=�$	��373��$��	10,75,0,0q�$	��Top=�$	��699��.@��� $��Nuevo Requerimiento��.��� ���	#FFE5E5E5.+��� -   $ label��$��Descripción: ��$
��Left=�$��7,35,0,0q�$	��Top=�$��16(�$��30��$	��100���� -    ��Name$ descriptionBox��$
��Left=�$��63��$��6,65,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��678��$��16(��� -   $ label1��$��Prioridad: ��$
��Left=�$��	7,141,0,0q�$	��Top=�$��89��$��16(�$��29���� -    ��Name$ requestSend��$��Enviar Requerimiento��$
��Left=�$��513,297,0,0q�$	��Top=�$	��171��$��16(�$��25���� -   $
 exit��$��Salir��$
��Left=�$��
26,297,0,0q�$	��Top=�$��94��$��16(�$��22���� -   $ label3��$��Orden de compra: ��$
��Left=�$��7,7,0,0q�$	��Top=�$��16(�$��30��$	��140���� -	   $ requestText��$
��Left=�$��23��$��
146,14,0,0q�$
 Wrap=�$�� ��$	��Top=�$��99��$��16(��� -
   $ label4��$��No. de Guía:��$
��Left=�$��
277,10,0,0q�$	��Top=�$��16(�$��29��$��95���� -   $ 	trackText��$
��Left=�$��23��$��
378,14,0,0q�$
 Wrap=�$�� ��$	��Top=�$��87��$��16(��� -   $ label5��$!��Fecha en puerto (Promesa): ��$
��Left=�$��
16,204,0,0q�$	��Top=�$��16(�$��29��$	��202��(  "System.Windows.Controls.DatePicker  -     Name$ 	deliverBy��$
��Left=�$��223,211,0,0q�$	��Top=�$	��116��$��22���� -   $ label6��$��	Carrier: ��$
��Left=�$��
500,10,0,0q�$	��Top=�$��16(�$��29��$��64���� -   $ attachFiles��$��Orden de Compra��$
��Left=�$��146,253,0,0q�$	��Top=�$	��113��$��23���� -    ��Name$ 
carrierBox��$
��Left=�$��
568,13,0,0q�$	��Top=�$	��118��$��24��.@��� $	��DHL���� $	��UPS���� $��Castores���� $��Estafeta���� -   $ buyerBox��$
��Left=�$��101,146,0,0q�$	��Top=�$	��117��$��24��.@��� $��	Expeditar���� $
��Alta���� $��Normal���� -   $ label5_Copy��$��Departamento: ��$
��Left=�$��255,141,0,0q�$	��Top=�$��16(�$��29��$	��118���� -   $ label5_Copy1��$��Total de Factura: ��$
��Left=�$��441,204,0,0q�$	��Top=�$��16(�$��29��$	��127���� -   $ deparmentBox��$
��Left=�$��378,146,0,0q�$	��Top=�$	��117��$��24��$	 �� IsSynchronizedWithCurrentItem$	 FalseY�.@��� $��Almacen���� $��
Maquinados���� $��Oficina���� $��	Soldadura���� -   $ 	totalFact��$
��Left=�$��21��$��568,212,0,0q�$
 Wrap=�$�� ��$	��Top=�$��98��$��16(��� -   $ importanceAnex��$��Anexo de importancias��$
��Left=�$��146,281,0,0q�$	��Top=�$	��113��$��22���� -   $ 
receiptBtn��$��Factura��$
��Left=�$��146,308,0,0q�$	��Top=�$	��113��$��23���� -   $ uploadedFilesLabel��$��Archivos cargados: 0��$
��Left=�$��277,281,0,0q�$	��Top=�$	��188���� $��Requerimientos��.��� ���	#FFE5E5E5.+��� -   
 ��Name$
 requestList��$
��Left=�$	��294��$��10,0,0,0q�$	��Top=�$	��674�� �� View �� .P��� $��Status�� �� Width$ 80�� �� DisplayMemberBinding �� *status+�� $��Usuario��$ 80�� �� *user+�� $��Orden de Compra��$	 120�� �� *	requestID+�� $��No. de Guía��$	 100�� �� *	trackID+�� $��Carrier��$ 80�� �� *	carrier+�� $��Descripción��$	 120�� �� *description+�� $��	Prioridad��$ 80�� �� *buyer+�� $��Departamento��$ 80�� �� *
department+�� $��Gastos��$ 80�� �� *
spentMoney+�� $��No. de archivos��$	 120�� �� *filesUploaded+�� $��Fecha en puerto��$	 120�� �� *deliverDate+�� $��Fecha de creacion��$	 120�� �� *realDeliverDate+�� -   $ refreshRequestList��$��
Actualizar��$
��Left=�$��309,316,0,0q�$	��Top=�$��75���� $��Pendientes a corregir��.��� ���	#FFE5E5E5.+��� -   $
 	errorList��$
��Left=�$	��290��$��	10,10,0,0q�$	��Top=�$	��674�� �� .P��� $��Status��$ 80�� �� *status+�� $��Usuario��$ 80�� �� *user+�� $��Orden de Compra��$	 120�� �� *	requestID+�� $��No. de Guía��$	 100�� �� *	trackID+�� $��Carrier��$ 80�� �� *	carrier+�� $��Descripción��$	 120�� �� *description+�� $��	Prioridad��$ 80�� �� *buyer+�� $��Departamento��$ 80�� �� *
department+�� $��Gastos��$ 80�� �� *
spentMoney+�� $��No. de archivos��$	 120�� �� *filesUploaded+�� $��Fecha en puerto��$	 120�� �� *deliverDate+�� $��Fecha de creacion��$	 120�� �� *realDeliverDate+�� -   $ refreshErrorList��$��
Actualizar��$
��Left=�$��311,316,0,0q�$	��Top=�$��75��