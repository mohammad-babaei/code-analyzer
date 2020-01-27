from flask import Flask, render_template
from flask_wtf import FlaskForm
from flask_codemirror.fields import CodeMirrorField
from wtforms.fields import SubmitField
from flask_codemirror import CodeMirror
from flask_bootstrap import Bootstrap
import subprocess
import json

SECRET_KEY = 'secret!'
CODEMIRROR_LANGUAGES = ['clike', 'html']
CODEMIRROR_THEME = 'mdn-like'
CODEMIRROR_ADDONS = (
     ('display','placeholder'),
)

app = Flask(__name__)
app.config.from_object(__name__)
codemirror = CodeMirror(app)
Bootstrap(app)

class MyForm(FlaskForm):
    source_code = CodeMirrorField(language='clike',
                                config={'lineNumbers' : 'true'})
    submit = SubmitField('Submit')

@app.route('/', methods = ['GET', 'POST'])
def index():
    form = MyForm()
    
    if form.source_code.data == None:
        sample = open('sampleToAnalize.cs', 'r')
        form.source_code.data = sample.read()

    csharp_output_json = None
    if form.validate_on_submit():
        inputText = form.source_code.data

        csharp_output_string = """{
                    "issues": [
                        {
                        "id": 3,
                        "type":"Single Responsibility",
                        "quote": "Functions should single responsi.....",
                        "author": "Robert C Martin",
                        "title": "Looks like your code breaks single responsibility in these areas:",
                        "warnings": [
                            {
                            "id": 1,
                            "value": "at (12, 10): void funct2(int a, int b) has multiple return statements"
                            },
                            {
                            "id": 2,
                            "value": "at (8, 10): int trace(int x, string foo) has multiple return statements"
                            }
                        ]
                        }
                    ]
                    }"""
        
        csharp_output_json = json.loads(csharp_output_string)
        
    return render_template('index.html', form=form, Issues = csharp_output_json)

@app.route('/aboutUs', methods=['GET', ])
def about_us():
    return render_template('about_us.html')

@app.route('/this-site', methods=['GET', ])
def about_site():
    return render_template('about_site.html')