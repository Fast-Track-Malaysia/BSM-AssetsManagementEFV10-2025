# Assets Management System — User Guide

BSM Assets Management (AMS) — Web Application User Guide

---

## 1. Introduction

### 1.1 About this system

The Assets Management System (AMS) is a Computerised Maintenance Management System (CMMS) used to plan, execute and track maintenance on physical assets across the organisation's facilities, retail outlets and terminals. It covers the full lifecycle from equipment registration through preventive and corrective maintenance, purchasing of spare parts and services, and compliance deviations.

The web application is the primary interface for day-to-day users. A separate Windows desktop application exists for administrative tasks, and a Windows service handles scheduled workflow execution.

### 1.2 Who should read this guide

This guide is intended for everyone who uses the AMS web application, including:

- **Requestors** raising work requests and purchase requests
- **Planners** and **Planner Groups** converting requests into work orders
- **Supervisors** and **Technicians** executing work and updating job status
- **Approvers** and **Managers** reviewing and approving documents
- **Deviation users, reviewers and approvers** managing OLAFD and SCE deviations
- **System administrators** configuring master data, users and roles

Chapter references for quick access:

- New to the system? Start with **Chapter 2 (Logging On)** and **Chapter 3 (Navigating)**.
- Setting up data for the first time? See **Chapter 4 (Master Data)**.
- Daily operations? See **Chapters 5 to 9**.
- Running reports? See **Chapter 10**.

### 1.3 Key concepts

The system is organised around a small set of document types, each identified by a two-letter code:

- **PM** — Preventive Work Order
- **CM** — Corrective (Breakdown) Work Order
- **WR** — Work Request
- **PR** — Purchase Request
- **DE** — Deviation

Documents flow through well-defined job and document statuses, and many of them post data to SAP Business One once approved.

---

## 2. Logging on

### 2.1 Accessing the application

Open a supported web browser and navigate to the AMS URL provided by your system administrator. The sign-in page (Login.aspx) will be displayed.

### 2.2 Signing in

Enter your **User Name** and **Password**, then click **Log On**. Usernames are case-insensitive. If you do not yet have an account, contact your administrator — accounts are created in the desktop administration application and cannot be self-registered.

> NOTE: Anonymous access is only permitted for the error page, images and the DXX.axd resource handler. All business screens require authentication.

### 2.3 First sign-in

On first sign-in you will be prompted to change your password if the administrator has flagged your account for it. Choose a strong password and keep it confidential — your account is linked to every document you create, approve or modify.

### 2.4 Signing out

Use the **Log Off** action on the toolbar (or simply close the browser). The session will also end automatically after the configured inactivity timeout.

### 2.5 Forgotten password

There is no self-service password reset. Contact your system administrator to have your password reset.

---

## 3. Navigating the application

### 3.1 The application layout

After signing in, the screen is divided into three areas:

- **Navigation pane** on the left, grouping menus and list views
- **Main work area** in the centre, showing either a list of records or a document's details
- **Toolbar** at the top, showing actions available for the current view

### 3.2 Navigation groups

Menu items are organised into the following groups. Exact visibility depends on your role.

- **Equipments** — registered equipment records
- **Location** — Areas, Locations, Sub Locations
- **Equipment Setup** — Equipment Classes, Component Classes, Equipment Groups, Component Groups, Equipment Class Properties, Criticalities, Check Lists, SCE Categories
- **PM Schedule Setup** — PM Classes, PM Departments, PM Frequencies, PM Schedules, PM Patches
- **Work Requests** — work requests raised by users
- **Work Orders** — preventive (PM) and corrective (CM) work orders
- **Purchase Requests** — spare parts and services requests (post to SAP B1)
- **Quotation** — purchase quotations
- **Deviation** — OLAFD and SCE deviations
- **Deviation Setup** — Discipline, Frequencies, Locations, Risk Assets, Risk People, Risk Environment, Risk Community, Risks
- **Setup** — Contract Documents, Contractors, Item Masters, Deviation WO Types, Deviation WR Types
- **System Setup** — Planner Groups, Positions, Priorities, Technicians, Job Statuses, Work Order Operation Types
- **Reports** — on-demand reports and KPI measures

### 3.3 List views

A list view shows multiple records in a grid. Common actions:

- **New** — create a new record
- **Edit** — open the highlighted record for editing
- **Delete** — remove the highlighted record (most transactional documents disable this — use Cancel instead)
- **Export** — export the current grid to Excel, PDF, CSV, RTF or XML
- **Search** — filter records by any text value

Columns can be reordered (drag the column header) and sorted (click the header). Use the filter row below the header to filter by column. The current layout is remembered per user.

### 3.4 Detail views

A detail view shows one record with all its fields, often on multiple tabs. Most documents in this system have tabs such as **Header**, **Detail**, **Attachments**, **Photos**, **Doc Status** (history) and **Job Status** (work orders only).

### 3.5 Data entry basics

- Fields marked with a red asterisk are mandatory.
- Lookup fields (Equipment, Contractor, etc.) support free-text search — type a few characters and press **Tab** or pick from the drop-down.
- Detail collections are edited directly in the grid inside each tab — use the **+** button to add a line, **x** to remove one, and the in-line editors to enter values.

### 3.6 Saving, cancelling and closing

- **Save** commits the current record and keeps it open.
- **Save and Close** commits and returns to the list.
- **Save and New** commits and starts a blank record of the same type.
- **Close** (without Save) discards unsaved changes after confirmation.

---

## 4. Master data setup

Before day-to-day transactions can be recorded, the underlying master data must be populated. Master data is normally set up once, then maintained as the organisation changes. This chapter covers the master data groups in the order a new site would typically configure them.

### 4.1 Location hierarchy

AMS organises physical sites as **Area → Location → Sub Location**. Each equipment record is attached to a Sub Location, so this hierarchy must exist before you register equipment.

To create an Area:

1. Expand the **Location** group in the navigation pane.
2. Click **Areas**, then **New**.
3. Enter **Code** and **Name**.
4. Click **Save and Close**.

Repeat the process for **Locations** (select the parent Area) and **Sub Locations** (select the parent Location).

### 4.2 Equipment classes and groups

Equipment is categorised by **Class** (for example, *Pumps*, *Compressors*, *Tanks*) and **Group** (a finer categorisation used for reporting).

- **Equipment Classes** — define the type of asset. You can attach class-level documents and properties (specifications that all equipment of this class will inherit).
- **Equipment Groups** — used for grouping in reports and list filters.
- **Component Classes** and **Component Groups** — same concepts for components fitted to equipment (for example, motor, impeller, gearbox).
- **Criticalities** — the criticality rating applied to equipment (commonly A / B / C or High / Medium / Low).
- **Check Lists** — reusable inspection checklists that can be attached to a PM Schedule.
- **SCE Categories** — the Safety Critical Element categories used when flagging an equipment record as an SCE for deviation purposes.

### 4.3 Equipment

Each physical asset is recorded as an Equipment record under **Navigation → Equipments**.

Key fields:

- **Full Code** — auto-generated equipment identifier
- **Equipment Class** — links to the inherited properties and documents
- **Location / Sub Location** — where the equipment physically sits
- **Criticality** — A/B/C rating
- **Status** — `UP` when the equipment is available, `DOWN` when it is offline
- **SCE flag and category** — mark as a Safety Critical Element if applicable
- **Components** — sub-assemblies under the equipment (Tab: Components)
- **Properties** — equipment-specific specs beyond the class-inherited ones (Tab: Properties)
- **Attachments / Photos** — documents and images (Tabs: Attachments, Photos)

To register new equipment:

1. Go to **Navigation → Equipments → Equipments** and click **New**.
2. Select the **Class** first — class properties will be pre-populated.
3. Pick the **Location** and **Sub Location**.
4. Enter the **Equipment Description**, **Criticality** and any other fields required at your site.
5. If the equipment has sub-components, add them under the **Components** tab.
6. Save the record. The Full Code is generated on save.

### 4.4 People and organisations

- **Positions** — job titles (Operator, Engineer, Planner, etc.)
- **Technicians** — the technicians available to be assigned to work orders
- **Planner Groups** — groups of planners, used for routing of WRs and POs
- **Contractors** — external contractors (optionally linked to a contract)
- **Companies** — internal company/branch records (HQ, facility, terminal, retail)
- **Contract Documents** — the contract under which a contractor is engaged, with start and end dates

### 4.5 Catalogue

- **Item Masters** — the list of stock items and services that may appear on Purchase Requests. Item codes here must match SAP B1 item codes if PR posting to SAP is enabled.
- **Job Statuses** — the set of job status codes that a work order moves through. Key codes used by the system:
  - `AP` — initial CM job status (Awaiting Planning)
  - `AA` — initial PM job status (Awaiting Assignment)
  - `TC` — closure job status (Technical Completion)
- **Priorities** — priority levels for WRs and WOs
- **Work Order Operation Types** — the nature of an operation line on a work order
- **Document Types** — reference data for PM, CM, PR, WR and DE (seeded automatically on first startup — normally no manual change needed)

### 4.6 PM Schedule setup

Before creating PM schedules you need the supporting data:

- **PM Classes** — classification of PM activities
- **PM Departments** — the department owning the PM
- **PM Frequencies** — the interval (Daily, Weekly, Monthly, Quarterly, Annual, etc.)

### 4.7 Deviation setup

Reference data required for the Deviation module:

- **Discipline** — the discipline raising the deviation (Mechanical, Electrical, Instrumentation, HSE, etc.)
- **Frequencies** — the deviation review frequency
- **Locations** — the location classifications used for deviations
- **Risk Assets / People / Environment / Community** — the risk tables referenced by the Risk Assessment section of a deviation
- **Risks** — the consolidated risk catalogue
- **Deviation WO Types** / **Deviation WR Types** — mappings that flag Work Orders or Work Requests as requiring deviation approval

---

## 5. Preventive maintenance (PM)

### 5.1 Concept

Preventive maintenance is driven by PM Schedules that are repeated at a defined frequency. At the due date, a PM Schedule generates a PM Work Order (document type `PM`) that is executed in the same way as any other work order.

### 5.2 Creating a PM Schedule

1. Go to **Navigation → PM Schedule Setup → PM Schedules** and click **New**.
2. Enter the **PM Class**, **PM Department** and **PM Frequency**.
3. Add the equipment to the **Detail** tab (for whole-equipment PMs) or the **Components** tab (for component-level PMs).
4. Attach any **Check Lists** on the **PM Schedule Checklist** tab.
5. Enter the first **due date** and, if required, the planned **duration** and **man-hours**.
6. Save. The Full Code is generated automatically.

### 5.3 PM Patches

A PM Patch is a batch that groups multiple PM Schedules so they can be generated together (for example, "Monthly Electrical PMs — March"). Use Patches when you want a single generation event to produce a consistent set of work orders.

### 5.4 Generating PM Work Orders

On or shortly before the due date, a user with the **GeneratePM** role converts schedules into work orders:

1. Navigate to **PM Schedules** or **PM Patches**.
2. Select the schedule(s) to generate.
3. Click the **Generate** action on the toolbar.
4. The system creates a new Work Order in the `PM` document type with details copied from the schedule.
5. A user with the **AcknowledgePM** role reviews and acknowledges the generated PM. The PM is then visible to planners in the normal Work Orders list.

### 5.5 Rescheduling and skipping

A schedule can be placed on hold or skipped for a given cycle from the schedule's detail view. Rescheduling updates the next due date and writes an entry to the schedule's Doc Status history.

---

## 6. Work Requests (WR)

### 6.1 When to raise a Work Request

Raise a Work Request whenever someone observes a problem, need or improvement opportunity that requires maintenance action but is not already covered by a PM Schedule. Typical examples are breakdown reports, minor repairs and ad-hoc service requests.

### 6.2 Creating a Work Request

1. Go to **Navigation → Work Requests → Work Requests** and click **New**.
2. Select the **Company** (facility/terminal/retail outlet) and **Priority**.
3. Pick the **Equipment** or **Equipment Component** the request concerns. At least one equipment or component line is required on the **Detail** / **Components** tabs.
4. Fill in the **Long Description** and any **Long Remarks** explaining the issue.
5. Attach photos under the **Photos** tab and supporting documents under the **Attachments** tab.
6. If the request is a MOC-relevant change that requires an approved deviation, link the deviation in the **Deviation** tab.
7. Save. The Doc Num is generated and the request enters the initial document status.

### 6.3 Work Request lifecycle

A Work Request typically flows through:

1. **Draft / Submitted** — the requestor creates and saves the record
2. **Under Review** — a WR Supervisor or Planner reviews
3. **Approved** — approved for conversion to a work order
4. **Work Order Created** — one or more Work Orders have been linked from this WR
5. **Closed** — all linked work orders are closed
6. **Cancelled** — cancelled before completion (requires the **CancelWRRole** role)

The exact statuses in use at your site are configured on the **Doc Status** tab of each WR. The history of transitions is preserved and visible there.

### 6.4 Validation rules

- A WR cannot be saved if it has both an "approved deviation" and "no deviation needed" flag set at the same time — these are mutually exclusive.
- A WR cannot be deleted from the UI. Use Cancel instead.
- Contractors cannot edit a Work Request while it is in a "contractor checking" state — the Edit action is hidden.

### 6.5 Converting a WR to a Work Order

When a reviewer has approved the WR, the Planner:

1. Opens the WR detail view.
2. Clicks **Create Work Order** on the toolbar.
3. The system creates a new CM Work Order, copying equipment, description and attachments from the WR, and links it back to the source WR.

---

## 7. Work Orders (WO)

### 7.1 PM vs CM

Work Orders come in two flavours:

- **PM (Preventive)** — generated from a PM Schedule, no Work Request
- **CM (Corrective)** — raised from a Work Request (WR)

Both share the same screen, toolbar and tabs; the `DocType` field indicates which kind.

### 7.2 The Work Order screen

Tabs on a Work Order:

- **Header** — numbering, dates, priority, planner group, company, source (WR or PM Schedule)
- **Detail** — the equipment lines covered by this WO
- **Components** — component-level lines
- **Operations** — planned operation steps (Operation Types) for each equipment line
- **Component Operations** — operation steps for component lines
- **Man Hours** — planned and actual man-hours by technician
- **Purchase Requests** — PRs raised from this WO for parts and services
- **Photos** / **Attachments** — supporting media and documents
- **Job Status** — the job status history (AP → TC etc.)
- **Doc Status** — the document status history
- **Doc PG** — document patch/group linkage
- **Deviations** — linked deviations (if any)
- **Long Description / Remarks** — free text notes

### 7.3 Planning a Work Order

Once the WO has been created (from a WR for CM, or from PM generation for PM), the Planner:

1. Reviews the **Detail** and **Components** tabs and adjusts as needed.
2. Assigns the **Planner Group**.
3. Plans each operation line under **Operations** / **Component Operations**.
4. Assigns technicians and planned hours under **Man Hours**.
5. Sets the **Plan Date** (requires the **WOPlanDateRole** role).
6. Saves. The WO moves to the next job status.

### 7.4 Executing a Work Order

The assigned Supervisor and Technicians:

1. Open the WO from the Work Orders list.
2. Record actual man-hours on the **Man Hours** tab.
3. Upload photos and attachments.
4. If spare parts are needed, create a **Purchase Request** from the WO — see **Chapter 8**.
5. Update the **Job Status** tab as work progresses — for example from `AP` (Awaiting Planning) to in-progress codes used at your site, and finally `TC` (Technical Completion) on closure.

### 7.5 Job status codes

Job statuses are configurable. The system defaults used by the application are:

- `AP` — initial CM job status (Awaiting Planning)
- `AA` — initial PM job status (Awaiting Assignment)
- `TC` — closure status (Technical Completion)

Additional codes (such as *In Progress*, *On Hold*, *Waiting Parts*) are added by your administrator under **System Setup → Job Statuses**.

### 7.6 Deviations on Work Orders

If the Work Order type is flagged in **Setup → Deviation WO Types** as requiring a deviation, the system enforces one of the following on save:

- Either an approved deviation is linked in the **Deviations** tab, **or**
- The WO is explicitly flagged as not requiring a deviation.

The system blocks saving a WO that has both an *approved* deviation and a *not-required* flag active at once.

### 7.7 Closing and cancelling

- **Close** — set the job status to `TC` and complete the WO. Closure requires all mandatory fields (actuals, attachments, etc. depending on site configuration) to be present.
- **Cancel** — cancel the WO before closure. Requires the **CancelWORole** role. A cancellation reason is recorded in the Doc Status history.
- **Delete** — deletion is blocked by a system rule. Use Cancel instead.

---

## 8. Purchase Requests (PR)

### 8.1 Purpose

A Purchase Request captures a request to procure materials or services needed to execute a Work Order. Once approved internally, PRs are **posted to SAP Business One** where procurement is carried out. Posting is controlled by the `B1Post` configuration flag — if disabled, PRs are tracked in AMS only.

### 8.2 Creating a PR from a Work Order

1. Open the Work Order in detail view.
2. Go to the **Purchase Requests** tab.
3. Click **+** to add a new PR.
4. Pick a **Contract Document** if the PR is raised against a contract — the contract dates will be validated against the PR date.
5. On the **Detail** tab of the PR, add line items from the **Item Masters** catalogue along with quantities and required dates.
6. Attach quotations and supporting documents on the **Attachments** tab.
7. Save. The Doc Num is generated.

### 8.3 Standalone PR

A PR may also be raised directly from **Navigation → Purchase Requests → Purchase Requests**, but the preferred pattern is to raise from the originating WO so traceability is preserved.

### 8.4 Posting to SAP B1

A user with the **PostPR** role opens an approved PR and clicks **Post to SAP**. The system:

1. Reads the SAP B1 connection settings (`B1Server`, `B1CompanyDB`, `B1UserName`, `B1Password`, `B1PRseries`) from the server configuration.
2. Logs into SAP using the `SAPbobsCOM` DI API.
3. Creates a Purchase Request document in SAP under the configured series.
4. Stores the AMS-to-SAP reference keys in SAP user-defined fields (`U_FT_AMS_PR`, `U_FT_AMS_PRNo`, `U_FT_AMS_WONo`, `U_FT_AMS_PRLINEID`, `U_Contract`, `U_ContractNo`).
5. Copies attachments from the AMS attachment folder to the SAP attachment path (`B1AttachmentPath`).

If posting fails, the error is shown in the UI and written to `eXpressAppFramework.log` on the server. The SAP PR is not created until a successful post.

### 8.5 Cancelling a PR

A user with the **CancelPRRole** role can cancel a PR. If the PR has already been posted to SAP, the SAP document is cancelled as well.

### 8.6 SAP PR view

The **Navigation → Purchase Requests → Vw SAP PR** item shows the mirror view of the PRs that exist in SAP — useful for reconciliation. This view is read-only.

---

## 9. Deviations (OLAFD and SCE)

### 9.1 Purpose

A Deviation captures an authorised departure from a standard (an OLAFD — Operating Limit And Fixed Deviation — or an SCE — Safety Critical Element — deviation). Deviations carry their own approval workflow separate from Work Orders and Work Requests, but they can be linked to WOs or WRs that depend on the deviation being in force.

### 9.2 Deviation types

- **OLAFD** — operating-limit deviation
- **SCE** — safety critical element deviation

Set the **Deviation Type** on the header when creating the record. The type drives which reviewers and approvers are required.

### 9.3 Creating a Deviation

New deviations are typically created by a user with the **DeviationUser** role from within the originating WO, WR or PM context. On the Deviation screen:

1. Select **Company**, **Deviation Type** (OLAFD or SCE), **Discipline**, **Location** and **Frequency**.
2. Enter the **effective dates** and the **reason / description**.
3. Complete the **Risk Assessment** sub-tabs: Risk to Assets, People, Environment, Community — each pulls from the matching Risk reference table.
4. Add the required reviewers on the **Reviewers** (Detail2) tab.
5. Add mitigation measures on the **Mitigations** (Detail) tab.
6. Upload supporting documents under **Attachments** (Detail3).
7. Save as **Draft**.

### 9.4 Deviation lifecycle (status flow)

A Deviation moves through these statuses (shown on the **Doc Status** tab):

- **New** — freshly created, not yet edited
- **Draft** — being prepared by the initiator
- **Under Review** — circulated to reviewers
- **Submit Acknowledge** — reviewers have acknowledged and the deviation has been submitted for approval
- **Pending Approval** — awaiting sign-off by the DeviationApprover
- **Approved** — approved and in force
- **Open** — currently effective
- **Expired** — effective period has ended; approval lapsed
- **Draft Extension** — an extension request is being prepared
- **Approved Extension** — an extension has been approved
- **Withdrawn** — withdrawn before approval
- **Closed** — formally closed after conclusion
- **Cancel** — cancelled before approval

### 9.5 Who does what

- **DeviationUser** — creates and edits deviations in Draft status
- **DeviationReviewer** — reviews deviations in Under Review status and records their acknowledgement
- **DeviationManager** — manages the deviations portfolio, can submit for approval
- **DeviationApprover** — approves deviations in Pending Approval status
- **DeviationReopen** — can re-open a Closed deviation

### 9.6 Actions available on a Deviation

From the toolbar of the Deviation detail view (exact visibility depends on status and role):

- **Submit for Review** — move Draft → Under Review
- **Acknowledge** — a reviewer marks their review complete
- **Submit for Approval** — move Under Review → Pending Approval
- **Approve** — move Pending Approval → Approved
- **Withdraw** — withdraw a deviation before approval
- **Request Extension** — start a Draft Extension from an Approved deviation
- **Approve Extension** — approve the extension
- **Close** — move Approved / Expired → Closed
- **Reopen** — move Closed → Open (requires DeviationReopen)

> NOTE: New, Link, Unlink and Delete are hidden on the Deviations list — deviations are only created from the contextual document (WO/WR) and are never deleted, only cancelled or withdrawn.

### 9.7 Linking deviations to WO / WR

A Work Order or Work Request that falls under a configured Deviation Type will show a **Deviations** tab. Add the relevant deviation there. The linked deviation's status is checked on save — see the validation rules in section 7.6.

---

## 10. Reports

### 10.1 The Reports module

Reports are defined as DevExpress `.repx` files and are registered in the application's Report Data library. Users invoke reports either from the **Reports** navigation group or directly from a document view via the **Print** or **Report** toolbar actions.

### 10.2 Built-in reports

The following are provided out of the box:

- **All Work Order Documents** — list of WOs with header and key fields
- **All Work Request Documents** — list of WRs
- **All PM Patch Documents** — PM patch summary
- **Bad Actor** — equipment with highest breakdown frequency
- **Weekly AMS Measures** — KPI measures for the current week
- **Monthly KPI Measures** — AI (Integrity), ME (Mechanical) and SCE flavours
- **Facility / Retail / Terminal Current AMS Measures** — site-scoped KPIs
- **Facility / Retail / Terminal ME Status Report** — mechanical integrity status
- **Facility / Retail / Terminal SCE Status Report** — SCE status
- **RETAIL / TERMINAL SCE Deviation LAFD Status Report** — deviation status for SCE items
- **Deviation Layout** — formatted deviation report for distribution

### 10.3 Running a report

1. Open **Navigation → Reports** and select the report.
2. A parameters dialog is displayed — typical parameters are **Company**, **Date From**, **Date To**, **Equipment Class** or **Discipline**.
3. Click **OK**. The report is rendered in the browser's report preview.
4. Use the preview toolbar to navigate pages, zoom, export or print.

### 10.4 Exporting

The preview toolbar supports export to PDF, XLSX, DOCX, MHT, RTF, XLS, CSV, TXT and image formats. For sharing by email, PDF or XLSX are preferred.

### 10.5 Ad-hoc reports from list views

Any list view can be exported directly without opening a report, using the **Export** toolbar action. This gives a raw grid dump in Excel or PDF that is useful for quick analyses where no formatted report exists.

---

## 11. Administration

This chapter is aimed at administrators. End users can skip it.

### 11.1 User and role administration

User accounts are managed in the **Admin → Users** area (visible only to users in the **Administrators** role). For each user:

- Assign a unique **User Name** and initial password
- Add the required **Roles** on the **Roles** tab
- Optionally link the user to a **Technician**, **Contractor** or **Position** record

### 11.2 Roles in use

The following roles are recognised by the application code. Assign them to users based on duties:

- **Administrators** — full access
- **Manager, Approver, Planner, Supervisor, Technician, Requestor, Contractor, WRSupervisor, WPS** — operational roles
- **GeneratePM** — may generate PM work orders from schedules
- **AcknowledgePM** — may acknowledge generated PM work orders
- **PostPR** — may post Purchase Requests to SAP B1
- **CancelPRRole / CancelWRRole / CancelWORole** — may cancel PRs / WRs / WOs
- **WOPlanDateRole** — may set the Plan Date on a Work Order
- **DeviationUser, DeviationReviewer, DeviationManager, DeviationApprover, DeviationReopen** — deviation workflow roles

Avoid granting the Administrators role to operational users.

### 11.3 Uploading a new report

New or updated `.repx` reports are uploaded via the built-in Reports UI:

1. Go to **Reports → Report Data** (administrator only).
2. Click **New**, enter a name, and upload the `.repx` file.
3. Save. The report is now visible in the Reports navigation group.

Dropping a `.repx` file on disk alone does **not** register the report — the upload step is required.

### 11.4 Configuration

Server-side configuration lives in `Web.config` on the web server. Key keys:

- **ConnectionString** — SQL Server connection for the AMS database (name must remain `ConnectionString`)
- **EmailSend / EmailHost / EmailPort / Email / EmailSSL / EmailUseDefaultCredential** — outbound email for notifications
- **B1Post** — `Y` to enable SAP B1 posting for Purchase Requests
- **B1Server / B1CompanyDB / B1UserName / B1Password / B1Sld / B1License / B1DbServerType / B1DbUserName / B1DbPassword / B1Language / B1PRseries** — SAP B1 DI API settings
- **B1AttachmentPath** — the SAP attachment folder that AMS copies PR attachments into
- **B1PRCol / B1PRNoCol / B1WONoCol / B1PRLineIDCol / B1CTCol / B1CTNoCol** — names of the user-defined fields on the SAP PR document used for traceability

Changes to these keys require a restart (IIS application pool recycle) to take effect. Credentials in `Web.config` must be protected by filesystem ACLs and the file should not be committed to source control with production values.

### 11.5 Database updates

Schema changes are applied in two stages:

1. **Entity Framework migrations** — the timestamped migrations under `AssetsManagementEF.Module/Migrations/` are applied automatically on application startup when a schema mismatch is detected in Debug mode. In Release mode, the administrator must apply the migration explicitly.
2. **Seed data** — the `ModuleUpdater` runs after the schema update and inserts any missing reference rows (document types, default roles, etc.).

Administrators should always take a SQL Server backup before deploying a new version.

### 11.6 Workflow service

Scheduled workflows (for example, overnight PM generation notifications) run inside the **AssetsManagementEF.WorkflowServerService** Windows Service. Installation instructions are in the `Install WorkflowServiceServer.docx` document in the project root. The service shares the same `ConnectionString` and `B1*` settings as the web application.

### 11.7 Troubleshooting

- **Cannot log in** — check the user account exists and is not locked; check that the correct Company / facility is assigned.
- **Error on Post PR to SAP** — verify `B1*` settings, confirm the SAP B1 server is reachable, verify the `B1PRseries` matches an existing active document series in SAP.
- **Report does not appear** — confirm the `.repx` has been uploaded via the Reports UI, not just copied to disk.
- **Date shows in wrong format** — the web application forces `dd/MM/yyyy`. Users should not change their browser regional settings to work around this.
- **"Cannot Delete" message** — delete is intentionally blocked on Equipment, Work Orders, Work Requests, PM Schedules and Deviations. Use Cancel or Withdraw.
- Application log: `eXpressAppFramework.log` in the web application folder on the server.

---

## 12. Glossary

- **AMS** — Assets Management System (this application)
- **BSM** — the parent company branding used throughout the product
- **CM** — Corrective Maintenance (breakdown work order)
- **CMMS** — Computerised Maintenance Management System
- **DE** — Deviation document type code
- **DI API** — SAP Business One Data Interface API, used by `SAPbobsCOM`
- **Doc Num** — the auto-generated document number shown in the header of every transaction
- **Job Status** — the execution status of a work order (for example `AP`, `AA`, `TC`)
- **KPI** — Key Performance Indicator
- **LAFD** — Life-cycle Assurance Failure Deviation
- **MOC** — Management Of Change
- **OLAFD** — Operating Limit And Fixed Deviation
- **PG** — Patch Group
- **PM** — Preventive Maintenance
- **PM Patch** — a named batch of PM Schedules generated together
- **PR** — Purchase Request
- **SCE** — Safety Critical Element
- **TC** — Technical Completion (closure job status)
- **WR** — Work Request
- **WO** — Work Order
- **XAF** — DevExpress eXpressApp Framework, the UI platform that the AMS web application is built on

---

*End of user guide.*
