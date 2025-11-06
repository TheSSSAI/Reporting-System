{
  "diagram_info": {
    "diagram_name": "User Authentication and Session Lifecycle Flow",
    "diagram_type": "flowchart",
    "purpose": "To visualize the end-to-end user login process, including credential validation, two-factor authentication (2FA), role-based redirection, and the subsequent session inactivity timeout workflow.",
    "target_audience": [
      "developers",
      "security analysts",
      "QA engineers",
      "product managers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "3 minutes"
  },
  "syntax_validation": {
    "syntax_validation": "Mermaid syntax verified and tested",
    "rendering_notes": "Optimized for both light and dark themes using classDefs for clarity."
  },
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Frontend UI",
      "Backend API",
      "Database"
    ],
    "key_processes": [
      "Credential Submission",
      "2FA Verification",
      "Role-based Redirection",
      "Session Timeout Warning",
      "Session Extension",
      "Automatic Logout"
    ],
    "decision_points": [
      "Credentials Valid?",
      "Account Locked?",
      "2FA Enabled?",
      "2FA Code Valid?",
      "User Role?",
      "Extend Session?"
    ],
    "success_paths": [
      "Successful login with/without 2FA and redirection to the correct dashboard.",
      "Successful session extension from the timeout warning modal."
    ],
    "error_scenarios": [
      "Invalid credentials",
      "Locked account",
      "Invalid 2FA code"
    ],
    "edge_cases_covered": [
      "Session timeout without user action",
      "Account lockout after multiple failed 2FA attempts"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "A flowchart detailing the user login process. It starts with credential submission, branches for 2FA validation, redirects based on user role, and shows a separate loop for session inactivity timeout and logout.",
    "color_independence": "Information is conveyed through text labels and flow, not just color.",
    "screen_reader_friendly": "All nodes have descriptive text labels for clear narration.",
    "print_compatibility": "Diagram renders clearly in black and white."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Scales appropriately for different screen sizes.",
    "theme_compatibility": "Works with default, dark, and neutral themes.",
    "performance_notes": "Diagram uses standard flowchart elements for fast rendering."
  },
  "usage_guidelines": {
    "when_to_reference": "During development of authentication features, security reviews, and creation of test plans for login and session management.",
    "stakeholder_value": {
      "developers": "Provides a clear map of the authentication logic and required state transitions.",
      "designers": "Validates the user journey through login and session expiration.",
      "product_managers": "Offers a comprehensive view of the security and user experience aspects of authentication.",
      "QA_engineers": "Defines all paths, including error conditions, that need to be tested."
    },
    "maintenance_notes": "Update this diagram if the authentication flow changes, such as adding new authentication factors or modifying session management rules.",
    "integration_recommendations": "Embed in developer documentation for authentication and security sections of the application."
  },
  "validation_checklist": [
    "✅ All critical user paths documented",
    "✅ Error scenarios and recovery paths included",
    "✅ Decision points clearly marked with conditions",
    "✅ Mermaid syntax validated and renders correctly",
    "✅ Diagram serves intended audience needs",
    "✅ Visual hierarchy supports easy comprehension",
    "✅ Styling enhances rather than distracts from content",
    "✅ Accessible to users with different visual abilities"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    subgraph "Login & Authentication Flow"
        A[Start: User on Login Page] --> B(User submits Username + Password)
        B --> C{Credentials Valid?}
        C -- No --> D[Increment Failed Login Count]
        D --> E{Account Locked?}
        E -- Yes --> F[Show 'Account Locked' Error]
        F --> A
        E -- No --> G[Show 'Invalid Credentials' Error]
        G --> A
        
        C -- Yes --> H[Reset Failed Login Count]
        H --> I{2FA Enabled?}
        
        I -- Yes --> J[Redirect to 2FA Page]
        J --> K(User submits 2FA Code)
        K --> L{2FA Code Valid?}
        L -- No --> D
        L -- Yes --> M{User Role?}
        
        I -- No --> M

        M -- Administrator --> N[Redirect to Control Panel]
        M -- Viewer --> O[Redirect to Report Viewer]
        
        N --> P((Authenticated Session))
        O --> P
    end

    subgraph "Session Inactivity Lifecycle"
        P --> Q{User Active?}
        Q -- Yes --> R[Reset Inactivity Timer]
        R --> P
        Q -- No --> S{Session Timeout Warning?}
        S -- Yes --> T(Show 'Stay Logged In?' Modal)
        T --> U{User clicks 'Stay Logged In'?}
        U -- Yes --> R
        S -- No --> V[Timer Continues]
        V --> P
        U -- No / Timeout --> W(Log Out User)
        W --> A
    end

    %% Styling
    classDef process fill:#e3f2fd,stroke:#1976d2,stroke-width:2px,color:#000
    classDef decision fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,color:#000
    classDef error fill:#ffebee,stroke:#d32f2f,stroke-width:2px,color:#000
    classDef success fill:#e8f5e9,stroke:#388e3c,stroke-width:2px,color:#000
    classDef state fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#000

    class B,D,H,K,R,T,W process
    class C,E,I,L,M,Q,S,U decision
    class F,G error
    class N,O,P success
    class A,J,V state
```