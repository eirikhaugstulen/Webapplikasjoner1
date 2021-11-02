import React from "react";
import {Button, Col, FormFeedback, FormGroup, Input, Label, Progress, Row} from "reactstrap";
import {Formik} from "formik";
import * as Yup from 'yup';
import {StrekningsVelger} from "./BestillForm/StrekningsVelger";
import {useBestillingsForm} from "../hooks/useBestillingsForm";
import history from "../../history";

// JSON vil byttes ut med API-kall i del 2
const reiselokasjoner = [
    {
        id: 1,
        displayName: 'Oslo',
    },
    {
        id: 2,
        displayName: 'Kristiansand',
    },
    {
        id: 3,
        displayName: 'Stavanger',
    },
    {
        id: 4,
        displayName: 'Bergen',
    },
    {
        id: 5,
        displayName: 'Ålesund',
    },
    {
        id: 6,
        displayName: 'Trondheim',
    }
]

export const Bestilling = () => {
    const [
        {
            avgangsstedState,
            ankomststedState,
            fraDatoState,
            tilDatoState,
            returState,
        },
        updateIsTouched,
    ] = useBestillingsForm();

    const today = new Date();
    today.setHours(0, 0, 0, 0)

    return (
        <>
            <Row form>
                <Col md={12}>
                    <p>Bestill reise</p>
                    <Progress multi>
                        <Progress
                            animated
                            bar
                            color={'info'}
                            value={30}
                        />
                    </Progress>
                </Col>
            </Row>
            <Formik
                initialValues={{
                    fraSted: avgangsstedState.avgangssted,
                    tilSted: ankomststedState.ankomststed,
                    avgang: fraDatoState.fraDato,
                    retur: returState.retur,
                    returDato: tilDatoState.tilDato,
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
                onSubmit={async values => alert(JSON.stringify(values, null, 2))}
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
                    } = props;
                    return (
                        <>
                            <Row
                                form
                                className={'mt-3'}
                            >
                                <StrekningsVelger
                                    reiselokasjoner={reiselokasjoner}
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
                                            type={'date'}
                                            id={'returDato'}
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
                                        <Label>Avgang før</Label>
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
                            </div>
                        </>
                    )
                }}
            </Formik>
        </>
    )
}
