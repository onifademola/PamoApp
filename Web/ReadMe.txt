-- PATIENTS Visit related tables
Attendance
Procedur
PhyExam
Rounds
patientComplaint
biovital
delivery
diagnosis
investigatn
nurseNote (related to UserTB)
scanReport
surgery
tbl_rec
ward
appointment (related to User TB (for Doctors))

-- Attendance related tables
**patientComplaint
appointment
consultingRoom


-- Process Flow --
1. Out patient walks in. Meets the front desk
2. Front desk gets patient's data and queues patient for OPD (vitals)
3. Patient moves to OPD, latest biovitals data are generated. Patient
   is queued by OPD at a selected consulting room
4a. Doctor in specified consulting room attends to patient
4b. Doctor may send patient for Lab Test or Scans
5a. Patient is admitted or sent to Pharmacy
5b. Drug/injection etc administered
5c. If patient is admitted, admission data is continually updated until discharged
6. Patient move to accounts to make payment
Process ends

--Process Statuses--
1. At OPD
2. At Consulting
3. Awaiting Lab/Scan/Test Result
4. On Admission
5. At Pharmacy
6. With Accounts
7. Done

****NEW TABLES ADDED TO APP****
ProcessFlow - to keep each stage of a visit(attendance)
QueueOPD - to store current Visits(attendances) waiting at OPD
QueueConsulting - to store current Visits(attendances) waiting to see a Doctor
QueueResult - to store current Visits(attendances) waiting to for Lab/Scan/Test Results to see a Doctor
QueueAdmission - to store current Visits(attendances) on admission
QueuePharmacy - to store current Visits(attendances) waiting at Pharmacy
QueueAccounts - to store current Visits(attendances) making payment for services


----TABLES WHOSE OLD DATA WILL BE LOST IN THE NEW APP----
These Tables will still be used, but modified
Reason: These tables have the wrong data relationship.
		They are directly related to the Patient table instead of the
		Attendance (Visit) Table

The involved tables are:
1. Surgery
2. ScanReport
3. Rounds (now WardRound)
4. Ward (now Admission)

########## REFACTORING TABLES ##########
1. Ward is renamed to Admission
2. Rounds is renamed to WardRound

REFACTORING NOTES:
-- WardRound is done on Admission, hence WardRound should be mapped to Admission Table, not Patient
