# Training task

## Requirements:

There was following requirement for this application

Technologie WPF , MVVM light

Create Main screen(dashboard), where will be 3 buttons.

1. First button(Users) will lead to new screen where will be gridview . Data in it will be loaded automatically from xml(users.xml). There will be this kind of information: Name, Age, Role(“John”,33, “Editor”). Prepare such xml yourselves.
   - Add functionality to add/edit/remove(up to you how) items from grid .
     - i. Keep in mind that: Age is positive number, Role is from list of Roles(Owner, Editor, Reader, Guest) and all values has to be filled
   - Add button to save those changes to xml with confirmation dialog.
   - Add button to load(reload) saved changes(so no need to go back and forth) with confirmation dialog.
   - Add button for navigating back to dashboard. This button should be part of navigation used on all screen.
2. Second button(Projects) from dashboard will led to another screen.

   - On beginning there will be dropdown with list of project name. Default value will be “not selected”. On same level will be button “Ok” to confirm this select.
     - i. After clicking “Ok” button load information about project to form
   - Bellow it will be form with 5 inputs – one text, one dropdown, one check box, one radiobox and one multiselect.if no project was selected it will be with empty or default values
   - Classic input will held project name
   - Dropdown (name:Project type) will have 3 values – intern, external, unknown. For each of them will be special behaviour affecting rest of items
   - Checkbox (name:Visible) cannot be edited and will be checked if project is external, otherwise it will be unchecked
   - Radiobutton (name:Known type) will have values yes/no. and will be set based on Project type. For intern/extern will be yes, otherwise no.

     - i. If someone change value it will affect dropdown Project type as well and vice versa if someone change Project type value will be set here as well

   - Multiselect (name: Possible roles) will have this values: Owner, Editor, Reader, Guest.

     - i. If project is Intern set just Owner

     - ii. If project is Extern set Owner, Editor and Reader

     - iii. If project is Unknown set Owner and Guest

   - At the end of this will be button with text Create if it is new or Save if it is editing existing
     - On beginning there will be no projects, after saving first create new source file for it where it will be stored. Its up to you to define it(XML, json, csv….anything what fits you best)

3. Third button will led to another screen. There will be just one dropdown – with names from XML + default value “not selected” as first value
   - After selecting any name, proper information(name, age, role) will be shown(up to you how)
   - Show list of project where can this user participate(his role is allowed like is described in 2.g)
