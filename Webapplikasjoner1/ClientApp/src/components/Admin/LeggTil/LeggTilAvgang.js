import React from "react";
import {Formik} from "formik";
import * as Yup from 'yup';
import {useGeneratedId} from "../../../Pages/hooks/useGeneratedId";
import {StatusMessage} from "../StatusMessage";
import {Button, FormFeedback, FormGroup, Input, Label} from "reactstrap";
import axios from "axios";
import qs from "qs";
import {checkUnauthorized} from "../../../utils/checkUnauthorized";

export const LeggTilAvgang = ({ strekning, refetch }) => {
    const { generatedId, refetchId } = useGeneratedId()

    return (
        <Formik
            initialValues={{
                Dato: '',
                Klokkeslett: '',
                Pris: '',
            }}
            onSubmit={async (values, formikHelpers) => {
                values.AvgangNummer = generatedId;
                values.Strekning = strekning;
                axios.post('/Avganger/Lagre', qs.stringify(values))
                    .then(() => refetch())
                    .catch(e => {
                        console.log(e?.response);
                        checkUnauthorized(e);
                        if (e?.response?.status === 400) {
                            formikHelpers.setErrors('Feil i inputvalidering')
                        }
                    })
                formikHelpers.resetForm()
                refetchId();
            }}
            validationSchema={Yup.object().shape({
                Dato: Yup.date().required('Påkrevd dato'),
                Klokkeslett: Yup.string().required('Påkrevd'),
                Pris: Yup.number('Må være et heltall').required('Påkrevd'),
            })}
        >
            {props => {
                const {
                    values,
                    touched,
                    errors,
                    status,
                    isSubmitting,
                    handleChange,
                    handleBlur,
                    handleSubmit,
                } = props;

                return (
                    <form onSubmit={handleSubmit}>
                        <StatusMessage status={status} />
                        <FormGroup>
                            <Label>Avgangsdato</Label>
                            <Input
                                id={'Dato'}
                                type={'date'}
                                value={values.Dato}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                invalid={errors.Dato && touched.Dato}
                            />
                            <FormFeedback>{errors.Dato}</FormFeedback>
                        </FormGroup>

                        <FormGroup>
                            <Label>Klokkeslett</Label>
                            <Input
                                id={'Klokkeslett'}
                                type={'time'}
                                value={values.Klokkeslett}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                invalid={errors.Klokkeslett && touched.Klokkeslett}
                            />
                            <FormFeedback>{errors.Klokkeslett}</FormFeedback>
                        </FormGroup>

                        <FormGroup>
                            <Label>Pris</Label>
                            <Input
                                id={'Pris'}
                                type={'number'}
                                value={values.Pris}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                invalid={errors.Pris && touched.Pris}
                            />
                            <FormFeedback>{errors.Pris}</FormFeedback>
                        </FormGroup>

                        <Button
                            outline
                            type={'submit'}
                            color={'success'}
                        >
                            Legg til avgang
                        </Button>
                    </form>
                )
            }}
        </Formik>
    )
}