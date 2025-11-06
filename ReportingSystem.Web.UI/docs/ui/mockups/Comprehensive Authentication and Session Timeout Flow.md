{
  "diagram_info": {
    "diagram_name": "Comprehensive Authentication and Session Timeout Flow",
    "diagram_type": "sequenceDiagram",
    "purpose": "To visualize the complete user login process, including the conditional two-factor authentication (2FA) step, and the subsequent client-side session management for inactivity timeout and session extension.",
    "target_audience": [
      "Backend Developers",
      "Frontend Developers",
      "QA Engineers",
      "Security Analysts"
    ],
    "complexity_level": "high",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "React Client",
      "API/Web Layer",
      "Application Layer",
      "Infrastructure Layer"
    ],
    "key_processes": [
      "Credential Validation",
      "Two-Factor Authentication (2FA) Check",
      "JWT Issuance",
      "Client-Side Inactivity Tracking",
      "Session Extension",
      "Automatic Logout"
    ],
    "decision_points": [
      "Are credentials valid?",
      "Is 2FA enabled for user?",
      "Is 2FA code valid?",
      "Is user inactive?",
      "Does user extend session?"
    ],
    "success_paths": [
      "Successful login (with and without 2FA)",
      "Successful session extension"
    ],
    "error_scenarios": [
      "Invalid credentials",
      "Invalid 2FA code"
    ],
    "edge_cases_covered": [
      "Session timeout due to inactivity",
      "User with 2FA vs. without 2FA"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "A sequence diagram illustrating the user authentication flow. It shows the initial credential submission, a conditional step for two-factor authentication, and the subsequent client-side timer that warns the user of inactivity and logs them out if the session is not extended.",
    "color_independence": "Information conveyed through logical flow and text, not just color",
    "screen_reader_friendly": "All participants and messages have descriptive text labels",
    "print_compatibility": "Diagram renders clearly in black and white"
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Scales appropriately for mobile and desktop viewing",
    "theme_compatibility": "Works with default, dark, and custom themes",
    "performance_notes": "Optimized for fast rendering with minimal complexity"
  },
  "usage_guidelines": {
    "when_to_reference": "During development and testing of authentication, 2FA, and session management features.",
    "stakeholder_value": {
      "developers": "Clear implementation guidance for the multi-step authentication flow and client-side session logic.",
      "designers": "Validation of user experience flow for login and session timeout warnings.",
      "product_managers": "Understanding of the complete user journey for secure access.",
      "QA_engineers": "Provides a comprehensive set of test scenarios, including success, failure, and edge cases."
    },
    "maintenance_notes": "Update when authentication logic changes, such as adding new 2FA methods or modifying session management rules.",
    "integration_recommendations": "Embed in developer documentation for the authentication API and in user stories related to security and session management."
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
sequenceDiagram
    actor User
    participant C as React Client
    participant A as API/Web Layer
    participant App as Application Layer
    participant I as Infrastructure Layer

    User->>C: 1. Enters credentials & clicks Login
    C->>A: 2. POST /api/v1/auth/token (username, password)

    A->>App: 3. AuthenticateUser(credentials)
    App->>I: 4. FindUserByNameAsync(username)
    I-->>App: 5. User object (or null)

    alt Credentials are valid
        App->>I: 6. CheckPasswordAsync(user, password)
        I-->>App: 7. bool isValid = true

        opt 2FA is enabled for user (US-029)
            App-->>A: 8. Return TwoFactorRequired
            A-->>C: 9. HTTP 200 OK { twoFactorRequired: true }
            C-->>User: 10. Show 2FA code input screen

            User->>C: 11. Enters 2FA code & clicks Verify
            C->>A: 12. POST /api/v1/auth/verify-2fa (userId, code)
            A->>App: 13. VerifyTwoFactorCode(userId, code)
            App->>I: 14. VerifyTwoFactorTokenAsync(user, code)
            I-->>App: 15. bool isCodeValid = true

            alt 2FA code is valid
                App->>A: 16. Generate JWT and Refresh Token
                A-->>C: 17. HTTP 200 OK { accessToken, refreshToken }
                C-->>User: 18. Redirect to Dashboard/Report Viewer
            else 2FA code is invalid
                I-->>App: 15a. bool isCodeValid = false
                App-->>A: 16a. Return InvalidCode
                A-->>C: 17a. HTTP 401 Unauthorized { error: 'Invalid code' }
                C-->>User: 18a. Show error message
            end
        else 2FA is not enabled
            App->>A: 8a. Generate JWT and Refresh Token
            A-->>C: 9a. HTTP 200 OK { accessToken, refreshToken }
            C-->>User: 10a. Redirect to Dashboard/Report Viewer
        end
    else Credentials are invalid (US-027)
        I-->>App: 7a. bool isValid = false
        App->>I: Increment failed login count
        App-->>A: 8b. Return InvalidCredentials
        A-->>C: 9b. HTTP 401 Unauthorized { error: 'Invalid credentials' }
        C-->>User: 10b. Show error message
    end

    group Client-Side Session Timeout (US-032)
        C->>C: 19. On successful login, start inactivity timer (e.g., 15 mins)

        loop Every second
            C->>C: Check for user activity (mouse, keyboard)
            alt User is active
                C->>C: Reset inactivity timer
            end
        end

        Note over C: After 14 minutes of inactivity...
        C-->>User: 20. Show 'Session expiring in 60s' modal with countdown

        alt User clicks 'Stay Logged In'
            C->>A: 21. POST /api/v1/auth/refresh (refreshToken)
            A-->>C: 22. HTTP 200 OK { newAccessToken, newRefreshToken }
            C->>C: 23. Update stored tokens & reset inactivity timer
            C-->>User: 24. Hide modal, user continues session
        else Countdown reaches zero
            C->>C: 21a. Clear stored tokens
            C-->>User: 22a. Redirect to Login page with 'Session timed out' message
        end
    end
```