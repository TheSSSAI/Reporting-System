import React, { useState } from 'react';
import {
    Container,
    Stepper,
    Step,
    StepLabel,
    Button,
    Box,
    Typography,
    CircularProgress,
    Alert,
} from '@mui/material';
import { useForm, FormProvider } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';

import { PageHeader } from '../widgets/PageHeader';
import { useReportsApi } from '../features/reports/api/useReportsApi';
import { useNavigate } from 'react-router-dom';

// These would be imported from feature components
const MetadataStep = () => <h2>Step 1: Metadata (Name, Description)</h2>;
const ConnectorStep = () => <h2>Step 2: Data Connector</h2>;
const TransformStep = () => <h2>Step 3: Data Transformation</h2>;
const OutputStep = () => <h2>Step 4: Output Format</h2>;
const DeliveryStep = () => <h2>Step 5: Delivery Destinations</h2>;
const ScheduleStep = () => <h2>Step 6: Schedule</h2>;
const ReviewStep = () => <h2>Step 7: Review and Save</h2>;

const steps = [
    'Metadata',
    'Data Connector',
    'Transformation',
    'Output',
    'Delivery',
    'Schedule',
    'Review',
];

// A simplified Zod schema for demonstration
const reportSchema = z.object({
    name: z.string().min(1, 'Report name is required'),
    description: z.string().optional(),
    connectorId: z.string().min(1, 'A data connector is required'),
    // ... add other fields as components are built
});

type ReportFormValues = z.infer<typeof reportSchema>;

const ReportConfigurationWizardPage: React.FC = () => {
    const navigate = useNavigate();
    const [activeStep, setActiveStep] = useState(0);
    const { createReport, isCreating } = useReportsApi();
    const [apiError, setApiError] = useState<string | null>(null);

    const methods = useForm<ReportFormValues>({
        resolver: zodResolver(reportSchema),
        mode: 'onChange', // or 'onBlur'
    });

    const handleNext = async () => {
        // In a real implementation, each step would have its own validation schema slice
        const isValid = await methods.trigger(['name', 'connectorId']); // example validation
        if (isValid) {
            setActiveStep((prevActiveStep) => prevActiveStep + 1);
        }
    };

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1);
    };

    const onSubmit = (data: ReportFormValues) => {
        setApiError(null);
        console.log('Submitting report configuration:', data);
        createReport(data as any, {
            onSuccess: () => {
                navigate('/reports');
                // show success toast
            },
            onError: (error) => {
                setApiError(error.response?.data?.error || 'Failed to create report.');
            },
        });
    };

    const getStepContent = (step: number) => {
        switch (step) {
            case 0:
                return <MetadataStep />;
            case 1:
                return <ConnectorStep />;
            case 2:
                return <TransformStep />;
            case 3:
                return <OutputStep />;
            case 4:
                return <DeliveryStep />;
            case 5:
                return <ScheduleStep />;
            case 6:
                return <ReviewStep />;
            default:
                return 'Unknown step';
        }
    };

    return (
        <Container maxWidth="lg">
            <PageHeader
                title="Create New Report"
                breadcrumbs={[{ label: 'Reports', path: '/reports' }, { label: 'Create' }]}
            />
            <Stepper activeStep={activeStep} sx={{ mb: 4 }}>
                {steps.map((label) => (
                    <Step key={label}>
                        <StepLabel>{label}</StepLabel>
                    </Step>
                ))}
            </Stepper>

            <FormProvider {...methods}>
                <form onSubmit={methods.handleSubmit(onSubmit)}>
                    {apiError && <Alert severity="error" sx={{ mb: 2 }}>{apiError}</Alert>}
                    
                    {/* Placeholder for showing step content */}
                    <Box sx={{ p: 3, border: '1px dashed grey', minHeight: 400 }}>
                        <Typography variant="h5" gutterBottom>{steps[activeStep]}</Typography>
                        {getStepContent(activeStep)}
                    </Box>
                    
                    <Box sx={{ display: 'flex', flexDirection: 'row', pt: 2 }}>
                        <Button
                            color="inherit"
                            disabled={activeStep === 0}
                            onClick={handleBack}
                            sx={{ mr: 1 }}
                        >
                            Back
                        </Button>
                        <Box sx={{ flex: '1 1 auto' }} />

                        {activeStep === steps.length - 1 ? (
                            <Button type="submit" variant="contained" disabled={isCreating}>
                                {isCreating ? <CircularProgress size={24} /> : 'Save Report'}
                            </Button>
                        ) : (
                            <Button onClick={handleNext} variant="contained">
                                Next
                            </Button>
                        )}
                    </Box>
                </form>
            </FormProvider>
        </Container>
    );
};

export default ReportConfigurationWizardPage;