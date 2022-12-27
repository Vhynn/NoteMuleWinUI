# NoteMule

  This is an application that was built in WinUI 3 to help IT support technicians take notes while working, it was intended to be lightweight while having a few key features to assist with the job.
  Originally the app was going to be built in MAUI instead of WinUI but MAUI lacks a rich text editor which was kind of the main part of what was needed because the notes taken often include screenshots as well as text.
  The features include a password generator, loadable templates for the main note editing page, and the ability to create edit and delete said templates. The non-note taking features can be navigated to through a toolbar at the top of the application.
  Unfortunately the built in WinUI rich text editor formats images in some way that is not compatible outside of the application (the word 'image' will be written instead of showing the actual image) and I could not find a way to fix that behaviour.
