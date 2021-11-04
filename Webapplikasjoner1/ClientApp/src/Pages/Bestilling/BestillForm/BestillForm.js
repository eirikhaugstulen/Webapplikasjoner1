import React from "react";
import {Button, Col, FormFeedback, FormGroup, Input, Label, Row} from "reactstrap";
import {Formik} from "formik";
import * as Yup from "yup";
import {StrekningsVelger} from "./StrekningsVelger";
import history from "../../../history";

export const BestillForm = ({ lokasjoner, setStep }) => {
    
    const today = new Date();
    today.setHours(0, 0, 0, 0)
    
    return (
        <>
            <Formik
                initialValues={{
                    fraSted: '',
                    tilSted: '',
                    avgang: '',
                    retur: '',
                    returDato: '',
                    klokkeslett: '',
                }}
                validationSchema={Yup.object().shape({
                    fraSted: Yup.string().required('Påkrevd'),
                    tilSted: Yup.string().required('Påkrevd'),
                    avgang: Yup.date().required('Avgangsdato er påkrevd').min(today, 'Avgang kan ikke være tilbake i tid'),
                    retur: Yup.boolean(),
                    returDato: Yup.date().when(['retur', 'avgang'], (retur, avgang, schema) => {
                            if (retur && avgang) {
                                return schema.required('Returdato er påkrevd').min(new Date(avgang), 'Returdato må være fram i tid')
                            }
                            if (retur) {
                                return schema.required('Returdato er påkrevd')
                            }
                        }
                    ),
                    klokkeslett: Yup.string(),
                })}
                onSubmit={async values => {
                    setStep(1)
                    alert(JSON.stringify(values, null, 2))
                }}
            >
                {props => {
                    const {
                        values,
                        touched,
                        errors,
                        isSubmitting,
                        handleChange,
                        handleBlur,
                        handleSubmit,
                        resetForm,
                    } = props;
                    return (
                        <>
                            <Row
                                form
                                className={'mt-3'}
                            >
                                <StrekningsVelger
                                    reiselokasjoner={lokasjoner}
                                    handleChange={handleChange}
                                    values={values}
                                    handleBlur={handleBlur}
                                    errors={errors}
                                    touched={touched}
                                />
                            </Row>

                            <Row form>
                                <Col md={12}>
                                    <FormGroup check>
                                        <Input
                                            type={'checkbox'}
                                            id={'retur'}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            invalid={errors.retur && touched.retur}
                                        />
                                        <Label>Retur?</Label>
                                    </FormGroup>
                                </Col>
                            </Row>

                            <Row form>
                                <Col md={6}>
                                    <FormGroup>
                                        <Label>Velg avgang</Label>
                                        <Input
                                            id={'avgang'}
                                            type={'date'}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            invalid={errors.avgang && touched.avgang}
                                        />
                                        <FormFeedback>{errors.avgang}</FormFeedback>
                                    </FormGroup>
                                </Col>

                                <Col md={6}>
                                    <FormGroup>
                                        <Label>Velg returdato</Label>
                                        <Input
                                            id={'returDato'}
                                            type={'date'}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            disabled={!values.retur}
                                            invalid={errors.returDato && touched.returDato}
                                        />
                                        <FormFeedback>{errors.returDato}</FormFeedback>
                                    </FormGroup>
                                </Col>
                            </Row>

                            <Row form>
                                <Col md={6}>
                                    <FormGroup>
                                        <Label>Avgang etter</Label>
                                        <Input
                                            type={'time'}
                                            id={'klokkeslett'}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            invalid={errors.klokkeslett && touched.klokkeslett}
                                        />
                                        <FormFeedback>{errors.klokkeslett}</FormFeedback>
                                    </FormGroup>
                                </Col>
                            </Row>

                            <div className={'mt-3 d-flex'} style={{gap: '5px'}}>
                                <Button
                                    color={'primary'}
                                    onClick={handleSubmit}
                                    disabled={isSubmitting}
                                >
                                    Søk
                                </Button>
                                <Button
                                    color={'secondary'}
                                    onClick={() => history.push("/")}
                                    outline
                                >
                                    Avbryt
                                </Button>
                                <Button
                                    onClick={() => {
                                        setStep(1);
                                        resetForm();
                                    }}
                                >
                                    Vis alle
                                </Button>
                            </div>
                        </>
                    )
                }}
            </Formik>
        </>
    )
}