import React from "react";
import {NavigationButton} from "../../components/Admin/NavigationButton";
import {Button, Col, Row} from "reactstrap";
import axios from "axios";
import history from "../../history";

export const AdminHome = () => (
    <div>
        <div className="row">
            <div className={'col-sm mt-2'}>
                <NavigationButton 
                    tittel={'Reiselokasjoner'}
                    tekst={'Legg til eller endre lokasjoner som ferjer kan reise fra og til. Disse lokasjonene er det som bygger opp reisestrekningene.'}
                    page={'lokasjoner'}
                />
            </div>
            <div className={'col-sm mt-2'}>
                <NavigationButton 
                    tittel={'Strekninger'}
                    tekst={'Legg til eller endre strekninger som ferjer reiser mellom. Disse strekningene blir maler for å sette opp flere avganger.'}
                    page={'strekninger'}
                />
            </div>
            <div className={'col-sm mt-2'}>
                <NavigationButton
                    tittel={'Avganger'}
                    tekst={'Legg til eller endre avganger. Avganger er strekninger som man kan sette opp til å gå flere ganger.'}
                    page={'avgang'}
                />
            </div>
        </div>
        <div className="row mt-4">
            <Col lg>
                <NavigationButton 
                    tittel={'Oppretting av Avgang'}
                    tekst={'For å opprette en Avgang i admin-grensesnittet må en strekning opprettes. \n' +
                    'Strekningen krever også at man oppretter to lokasjoner (Avgangslokasjon og Ankomstlokasjon), \n' +
                    'som strekingen skal gå mellom. Disse kan man opprette i sidene med samme navn. \n\n' +
                    'For å slette en Strekning så må også alle avgangene som bruker strekningen være slettet på forhånd. \n' +
                    'For eksempel om man skal slette Oslo-Bergen, så må alle avganger som bruker denne strekningen slettes \n' +
                    'før dette kan skje. Det samme gjelder også Lokasjon -> Strekning, hvor man må slette strekningen før man sletter lokasjonen.'
                    }
                />
            </Col>
        </div>
        <Row>
            <Col md={4} />
            <Col md={4} className={'text-center mt-3 '}>
                <Button
                    outline
                    onClick={() => {
                        axios.get('/Admins/LoggUt')
                            .then(() => {
                                history.push('/')
                            })
                            .catch(e => console.log(e))
                    }}
                >
                    Logg ut
                </Button>
            </Col>
            <Col md={4} />
        </Row>
    </div>
);